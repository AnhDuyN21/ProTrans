using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.ImageShippings;
using Application.ViewModels.ImageShippingDTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				var ImageShippings = await _unitOfWork.ImageShippingRepository.GetAllAsync();
				var ImageShippingDTOs = _mapper.Map<List<ImageShippingDTO>>(ImageShippings);

				if (ImageShippingDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get all successfully.";
					response.Data = ImageShippingDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No ImageShipping exists.";
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

			var ImageShipping = await _unitOfWork.ImageShippingRepository.GetByIdAsync(id);
			if (ImageShipping == null)
			{
				response.Success = false;
				response.Message = "ImageShipping is not existed.";
			}
			else
			{
				response.Success = true;
				response.Message = "ImageShipping found.";
				response.Data = _mapper.Map<ImageShippingDTO>(ImageShipping);
			}
			return response;
		}

		public async Task<ServiceResponse<ImageShippingDTO>> CreateImageShippingAsync(CUImageShippingDTO CUImageShippingDTO)
		{
			var response = new ServiceResponse<ImageShippingDTO>();
			try
			{
				var ImageShipping = _mapper.Map<ImageShipping>(CUImageShippingDTO);

				await _unitOfWork.ImageShippingRepository.AddAsync(ImageShipping);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					var ImageShippingDTO = _mapper.Map<ImageShippingDTO>(ImageShipping);
					response.Data = ImageShippingDTO;
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

		public async Task<ServiceResponse<bool>> DeleteImageShippingAsync(Guid id)
		{
			var response = new ServiceResponse<bool>();

			var ImageShipping = await _unitOfWork.ImageShippingRepository.GetByIdAsync(id);
			if (ImageShipping == null)
			{
				response.Success = false;
				response.Message = "Delete fail.";
				return response;
			}
			try
			{
				_unitOfWork.ImageShippingRepository.SoftRemove(ImageShipping);

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
using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.ImageShippings;
using Application.ViewModels.ImageShippingDTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				var ImageShippings = await _unitOfWork.ImageShippingRepository.GetAllAsync();
				var ImageShippingDTOs = _mapper.Map<List<ImageShippingDTO>>(ImageShippings);

				if (ImageShippingDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get all successfully.";
					response.Data = ImageShippingDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No ImageShipping exists.";
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

			var ImageShipping = await _unitOfWork.ImageShippingRepository.GetByIdAsync(id);
			if (ImageShipping == null)
			{
				response.Success = false;
				response.Message = "ImageShipping is not existed.";
			}
			else
			{
				response.Success = true;
				response.Message = "ImageShipping found.";
				response.Data = _mapper.Map<ImageShippingDTO>(ImageShipping);
			}
			return response;
		}

		public async Task<ServiceResponse<ImageShippingDTO>> CreateImageShippingAsync(CUImageShippingDTO CUImageShippingDTO)
		{
			var response = new ServiceResponse<ImageShippingDTO>();
			try
			{
				var ImageShipping = _mapper.Map<ImageShipping>(CUImageShippingDTO);

				await _unitOfWork.ImageShippingRepository.AddAsync(ImageShipping);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					var ImageShippingDTO = _mapper.Map<ImageShippingDTO>(ImageShipping);
					response.Data = ImageShippingDTO;
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

		public async Task<ServiceResponse<bool>> DeleteImageShippingAsync(Guid id)
		{
			var response = new ServiceResponse<bool>();

			var ImageShipping = await _unitOfWork.ImageShippingRepository.GetByIdAsync(id);
			if (ImageShipping == null)
			{
				response.Success = false;
				response.Message = "Delete fail.";
				return response;
			}
			try
			{
				_unitOfWork.ImageShippingRepository.SoftRemove(ImageShipping);

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
