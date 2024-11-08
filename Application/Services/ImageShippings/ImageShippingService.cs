using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.ImageShippings;
using Application.ViewModels.DocumentDTOs;
using Application.ViewModels.ImageShippingDTOs;
using Application.ViewModels.OrderDTOs;
using Application.ViewModels.TransactionDTOs;
using AutoMapper;
using Domain.Entities;
using System.Data.Common;
using System.Transactions;

namespace Application.Services.ImageShippings
{
	public class ImageShippingService : IImageShippingService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public ImageShippingService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<IEnumerable<ImageShippingDTO>>> GetAllImageShippingsAsync()
		{
			var response = new ServiceResponse<IEnumerable<ImageShippingDTO>>();

			try
			{
				var imageShippings = await _unitOfWork.ImageShippingRepository.GetAllAsync();
				var imageShippingDTOs = _mapper.Map<List<ImageShippingDTO>>(imageShippings);

				if (imageShippingDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get all successfully.";
					response.Data = imageShippingDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "Not exist.";
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

		public async Task<ServiceResponse<IEnumerable<ImageShippingDTO>>> GetImageShippingsByAssignmentShippingIdAsync(Guid id)
		{
			var response = new ServiceResponse<IEnumerable<ImageShippingDTO>>();

			try
			{
				var imageShippings = await _unitOfWork.ImageShippingRepository.GetAllAsync(x => x.AssignmentShippingId == id && x.UrlPath == null);
				var imageShippingDTOs = _mapper.Map<List<ImageShippingDTO>>(imageShippings);
				foreach (var i in imageShippingDTOs)
				{
					var document = await _unitOfWork.DocumentRepository.GetByIdAsync(i.DocumentId);
					var documentDTO = _mapper.Map<DocumentDTO>(document);
					i.Document = documentDTO;
				}

				if (imageShippingDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get successfully.";
					response.Data = imageShippingDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "Not exist.";
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

		public async Task<ServiceResponse<ImageShippingDTO>> GetImageShippingByIdAsync(Guid id)
		{
			var response = new ServiceResponse<ImageShippingDTO>();

			var imageShipping = await _unitOfWork.ImageShippingRepository.GetByIdAsync(id);
			if (imageShipping == null)
			{
				response.Success = false;
				response.Message = "Not exist.";
			}
			else
			{
				response.Success = true;
				response.Message = "Found.";
				response.Data = _mapper.Map<ImageShippingDTO>(imageShipping);
			}
			return response;
		}

		public async Task<ServiceResponse<ImageShippingDTO>> CreateImageShippingAsync(CreateImageShippingDTO createImageShipping)
		{
			var response = new ServiceResponse<ImageShippingDTO>();
			try
			{
				var imageShipping = _mapper.Map<ImageShipping>(createImageShipping);

				await _unitOfWork.ImageShippingRepository.AddAsync(imageShipping);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					var imageShippingDTO = _mapper.Map<ImageShippingDTO>(imageShipping);
					response.Data = imageShippingDTO;
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

		public async Task<ServiceResponse<ImageShippingDTO>> UpdateImageShippingAsync(Guid id, UpdateImageShippingDTO updateImageShipping)
		{
			var response = new ServiceResponse<ImageShippingDTO>();
			try
			{
				var imageShipping = await _unitOfWork.ImageShippingRepository.GetByIdAsync(id);

				if (imageShipping == null)
				{
					response.Success = false;
					response.Message = "Not exist.";
					return response;
				}
				var result = _mapper.Map(updateImageShipping, imageShipping);

				_unitOfWork.ImageShippingRepository.Update(imageShipping);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Data = _mapper.Map<ImageShippingDTO>(result);
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

		public async Task<ServiceResponse<bool>> UpdateImageAsync(Guid id, string urlPath)
		{
			var response = new ServiceResponse<bool>();
			try
			{
				var imageShipping = await _unitOfWork.ImageShippingRepository.GetByIdAsync(id);

				if (imageShipping == null)
				{
					response.Success = false;
					response.Message = "Not exist.";
					return response;
				}

				imageShipping.UrlPath = urlPath;

				_unitOfWork.ImageShippingRepository.Update(imageShipping);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Data = true;
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

		public async Task<ServiceResponse<bool>> DeleteImageShippingAsync(Guid id)
		{
			var response = new ServiceResponse<bool>();

			var imageShipping = await _unitOfWork.ImageShippingRepository.GetByIdAsync(id);
			if (imageShipping == null)
			{
				response.Success = false;
				response.Message = "Delete fail.";
				return response;
			}
			try
			{
				_unitOfWork.ImageShippingRepository.SoftRemove(imageShipping);

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
	}
}
