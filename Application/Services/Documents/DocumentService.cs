﻿using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Documents;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.DocumentDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System.Data.Common;
using System.Net.WebSockets;

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
				var sortDocuments = documents.OrderByDescending(d => d.CreatedDate).ToList();
				var documentDTOs = _mapper.Map<List<DocumentDTO>>(sortDocuments);

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

		public async Task<ServiceResponse<IEnumerable<DocumentGetByOrderIdDTO>>> GetDocumentsToBeNotarizedByAgencyIdAsync(Guid id)
		{
			var response = new ServiceResponse<IEnumerable<DocumentGetByOrderIdDTO>>();
			var targetDocuments = new List<DocumentGetByOrderIdDTO>();
			try
			{
				var documents = await _unitOfWork.DocumentRepository.GetAllAsync(x => x.NotarizationRequest && x.TranslationStatus == DocumentTranslationStatus.Translated.ToString() && x.NotarizationStatus == DocumentNotarizationStatus.PickedUp.ToString());
				foreach (var doc in documents)
				{
					var order = await _unitOfWork.OrderRepository.GetAsync(x => x.Id == doc.OrderId);
					if (order != null && order.AgencyId == id)
					{
						var mappedDocument = _mapper.Map<DocumentGetByOrderIdDTO>(doc);
                        mappedDocument.Deadline = (DateTime)order.Deadline;
						targetDocuments.Add(mappedDocument);
					}
				}

				if (targetDocuments.Count != 0)
				{
					response.Success = true;
					response.Message = "Get documents successfully.";
					response.Data = targetDocuments;
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
			var statuses = await _unitOfWork.DocumentStatusRepository.GetAllAsync(x => x.DocumentId == id);
			var documentDTO = _mapper.Map<DocumentDTO>(document);
			var documentStatusDTOs = _mapper.Map<List<DocumentStatusDTO>>(statuses);
			documentDTO.DocumentStatus = documentStatusDTOs;
			if (document == null)
			{
				response.Success = false;
				response.Message = "Document is not existed.";
			}
			else
			{
				response.Success = true;
				response.Message = "Document found.";
				response.Data = documentDTO;
			}
			return response;
		}

		public async Task<ServiceResponse<DocumentDTO>> CreateDocumentAsync(CreateDocumentDTO CUdocumentDTO)
		{
			var response = new ServiceResponse<DocumentDTO>();
			try
			{
				if (CUdocumentDTO.FileType == "Hard")
				{
					bool isExist = true;
					do
					{
						CUdocumentDTO.Code = GenerateRandomNumberSequence();
						var checkCode = await _unitOfWork.DocumentRepository.GetAsync(x => x.Code == CUdocumentDTO.Code);

						if (checkCode == null)
						{
							isExist = false;
						}
					}
					while (isExist);
				}


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
				var documentHistoryList = new List<DocumentHistory>();

				foreach (var property in properties)
				{
					var newValue = property.GetValue(CUdocumentDTO);
					var oldValue = typeof(Document).GetProperty(property.Name)?.GetValue(document);

					if (!Equals(newValue, oldValue) && newValue != null)
					{
						documentHistoryList.Add(new DocumentHistory
						{
							DocumentId = id,
							Name = property.Name,
							oldValue = oldValue?.ToString(),
						});
					}

					if (newValue == null)
					{
						typeof(UpdateDocumentDTO).GetProperty(property.Name)?.SetValue(CUdocumentDTO, oldValue);
					}
				}
				if (documentHistoryList.Any())
				{
					foreach (var documentHistory in documentHistoryList)
					{
						await _unitOfWork.DocumentHistoryRepository.AddAsync(documentHistory);
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

		public async Task<ServiceResponse<IEnumerable<DocumentGetByOrderIdDTO>>> GetDocumentsByOrderIdAsync(Guid id)
		{
			var response = new ServiceResponse<IEnumerable<DocumentGetByOrderIdDTO>>();

			try
			{
				var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Đơn hàng không tồn tại.";
                    return response;
                }
				var deadline = order.Deadline;

                var documents = await _unitOfWork.DocumentRepository.GetByOrderIdAsync(id);

				var documentDTOs = _mapper.Map<List<DocumentGetByOrderIdDTO>>(documents)
										.Select(dto => { dto.Deadline = (DateTime)deadline; return dto; })
										.ToList();

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

		public async Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetDocumentsByRequestIdAsync(Guid id)
		{
			var response = new ServiceResponse<IEnumerable<DocumentDTO>>();

			try
			{
				var documents = await _unitOfWork.DocumentRepository.GetAllAsync(x => x.RequestId == id);
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

		public async Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetDocumentsToBeNotarizedByOrderIdAsync(Guid id)
		{
			var response = new ServiceResponse<IEnumerable<DocumentDTO>>();

			try
			{
				var documents = await _unitOfWork.DocumentRepository.GetByOrderIdAsync(id);
				var targetDocuments = documents.Where(x => x.NotarizationRequest).ToList();
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
		//DocumentHistory
		public async Task<ServiceResponse<IEnumerable<DocumentHistoryDTO>>> GetDocumentHistoryByDocumentIdAsync(Guid documentId)
		{
			var response = new ServiceResponse<IEnumerable<DocumentHistoryDTO>>();

			try
			{
				var documentHistory = await _unitOfWork.DocumentHistoryRepository.GetAllAsync(x => x.DocumentId == documentId);
				var documentHistoryDTOs = _mapper.Map<List<DocumentHistoryDTO>>(documentHistory);

				if (documentHistoryDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get successfully.";
					response.Data = documentHistoryDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No document history exists.";
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
		public async Task<ServiceResponse<DocumentHistoryDTO>> GetDocumentHistoryByIdAsync(Guid id)
		{
			var response = new ServiceResponse<DocumentHistoryDTO>();

			try
			{
				var documentHistory = await _unitOfWork.DocumentHistoryRepository.GetByIdAsync(id);
				var documentHistoryDTO = _mapper.Map<DocumentHistoryDTO>(documentHistory);

				if (documentHistoryDTO != null)
				{
					response.Success = true;
					response.Message = "Get successfully.";
					response.Data = documentHistoryDTO;
				}
				else
				{
					response.Success = true;
					response.Message = "No document history exists.";
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
		//DocumentPrice
		public async Task<ServiceResponse<DocumentPriceDTO>> GetDocumentPriceByDocumentId(Guid documentId)
		{
			var response = new ServiceResponse<DocumentPriceDTO>();
			try
			{
				var documentPrice = await _unitOfWork.DocumentPriceRepository.GetAsync(x => x.DocumentId == documentId);
				var documentPriceDTO = _mapper.Map<DocumentPriceDTO>(documentPrice);

				if (documentPriceDTO == null)
				{
					response.Success = true;
					response.Message = "No document price exists.";
					return response;
				}

				response.Success = true;
				response.Message = "Get successfully.";
				response.Data = documentPriceDTO;
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
			}
			return response;
		}
		public async Task<ServiceResponse<CreateDocumentPriceDTO>> CreateDocumentPriceAsync(CreateDocumentPriceDTO createDocumentPriceDTO)
		{
			var response = new ServiceResponse<CreateDocumentPriceDTO>();
			try
			{
				var checkExist = await _unitOfWork.DocumentPriceRepository.GetAsync(x => x.DocumentId == createDocumentPriceDTO.DocumentId);
				if (checkExist != null)
				{

					response.Success = false;
					response.Message = "Document id already have DocumentPrice, cannot create new.";
					return response;
				}
				var documentPrice = _mapper.Map<DocumentPrice>(createDocumentPriceDTO);

				await _unitOfWork.DocumentPriceRepository.AddAsync(documentPrice);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					var documentPriceDTO = _mapper.Map<CreateDocumentPriceDTO>(documentPrice);
					response.Data = documentPriceDTO;
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
		public async Task<ServiceResponse<UpdateDocumentPriceDTO>> UpdateDocumentPriceAsync(Guid id, UpdateDocumentPriceDTO updateDocumentPriceDTO)
		{
			var response = new ServiceResponse<UpdateDocumentPriceDTO>();
			try
			{
				var checkExist = await _unitOfWork.DocumentPriceRepository.GetAsync(x => x.Id == id);
				if (checkExist == null)
				{

					response.Success = false;
					response.Message = " id not exist.";
					return response;
				}
				var objectToUpdate = _mapper.Map(updateDocumentPriceDTO, checkExist);

				_unitOfWork.DocumentPriceRepository.Update(checkExist);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					var documentPriceDTO = _mapper.Map<UpdateDocumentPriceDTO>(objectToUpdate);
					response.Data = documentPriceDTO;
					response.Success = true;
					response.Message = "Update successfully.";
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
		public string GenerateRandomNumberSequence()
		{
			Random random = new Random();
			string result = "";

			for (int i = 0; i < 6; i++)
			{
				int digit = random.Next(0, 10);
				result += digit.ToString();
			}

			return result;
		}

	}
}
