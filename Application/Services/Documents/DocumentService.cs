using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Documents;
using Application.ViewModels.DocumentDTOs;
using AutoMapper;
using Domain.Entities;
using System.Data.Common;

namespace Application.Services.Documents
{
	public class DocumentService : IDocumentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public DocumentService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetAllDocumentsAsync()
		{
			var response = new ServiceResponse<IEnumerable<DocumentDTO>>();

			try
			{
				var documents = await _unitOfWork.DocumentRepository.GetAllAsync();
				var documentDTOs = _mapper.Map<List<DocumentDTO>>(documents);

				if (documentDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get all successfully.";
					response.Data = documentDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No document exists.";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
			}
			return response;
		}

		public async Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetDocumentsToBeNotarizatedAsync()
		{
			var response = new ServiceResponse<IEnumerable<DocumentDTO>>();

			try
			{
				var documents = await _unitOfWork.DocumentRepository.GetAllAsync();
				var targetDocuments = documents.Where(x => x.NotarizationStatus == "Processing" && x.TranslationStatus == "Translated").ToList();
				var documentDTOs = _mapper.Map<List<DocumentDTO>>(targetDocuments);

				if (documentDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get documents successfully.";
					response.Data = documentDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No document exists.";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
			}
			return response;
		}

		public async Task<ServiceResponse<DocumentDTO>> GetDocumentByIdAsync(Guid id)
		{
			var response = new ServiceResponse<DocumentDTO>();

			var document = await _unitOfWork.DocumentRepository.GetByIdAsync(id);
			if (document == null)
			{
				response.Success = false;
				response.Message = "Document is not existed.";
			}
			else
			{
				response.Success = true;
				response.Message = "Document found.";
				response.Data = _mapper.Map<DocumentDTO>(document);
			}
			return response;
		}

		public async Task<ServiceResponse<DocumentDTO>> CreateDocumentAsync(CreateDocumentDTO CUdocumentDTO)
		{
			var response = new ServiceResponse<DocumentDTO>();
			try
			{
				var document = _mapper.Map<Document>(CUdocumentDTO);

				await _unitOfWork.DocumentRepository.AddAsync(document);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					var documentDTO = _mapper.Map<DocumentDTO>(document);
					response.Data = documentDTO;
					response.Success = true;
					response.Message = "Create successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error saving.";
				}
			}
			catch (DbException ex)
			{
				response.Success = false;
				response.Message = "Database error.";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { ex.Message };
			}

			return response;
		}

		public async Task<ServiceResponse<bool>> DeleteDocumentAsync(Guid id)
		{
			var response = new ServiceResponse<bool>();

			var document = await _unitOfWork.DocumentRepository.GetByIdAsync(id);
			if (document == null)
			{
				response.Success = false;
				response.Message = "Delete fail.";
				return response;
			}
			try
			{
				_unitOfWork.DocumentRepository.SoftRemove(document);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Success = true;
					response.Message = "Delete successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error deleting.";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { ex.Message };
			}

			return response;
		}

		public async Task<ServiceResponse<DocumentDTO>> UpdateDocumentAsync(Guid id, UpdateDocumentDTO CUdocumentDTO)
		{
			var response = new ServiceResponse<DocumentDTO>();
			try
			{
				var document = await _unitOfWork.DocumentRepository.GetByIdAsync(id);

				if (document == null)
				{
					response.Success = false;
					response.Message = "Document is not existed.";
					return response;
				}

				var properties = typeof(UpdateDocumentDTO).GetProperties();
				foreach (var property in properties)
				{
					var newValue = property.GetValue(CUdocumentDTO);
					var oldValue = typeof(Document).GetProperty(property.Name)?.GetValue(document);

					if (newValue == null)
					{
						typeof(UpdateDocumentDTO).GetProperty(property.Name)?.SetValue(CUdocumentDTO, oldValue);
					}
				}

				var result = _mapper.Map(CUdocumentDTO, document);

				_unitOfWork.DocumentRepository.Update(document);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Data = _mapper.Map<DocumentDTO>(result);
					response.Success = true;
					response.Message = "Update successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error updating.";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}

		public async Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetDocumentsByOrderIdAsync(Guid id)
		{
			var response = new ServiceResponse<IEnumerable<DocumentDTO>>();

			try
			{
				var documents = await _unitOfWork.DocumentRepository.GetByOrderIdAsync(id);
				var documentDTOs = _mapper.Map<List<DocumentDTO>>(documents);

				if (documentDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get successfully.";
					response.Data = documentDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No document exists.";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
			}
			return response;
		}
	}
}