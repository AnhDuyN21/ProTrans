using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Shippings;
using Application.ViewModels.ShippingDTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Shippings
{
	public class ShippingService : IShippingService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public ShippingService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<IEnumerable<ShippingDTO>>> GetAllShippingsAsync()
		{
			var response = new ServiceResponse<IEnumerable<ShippingDTO>>();

			try
			{
				var shippings = await _unitOfWork.ShippingRepository.GetAllAsync();
				var shippingDTOs = _mapper.Map<List<ShippingDTO>>(shippings);

				if (shippingDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get all successfully.";
					response.Data = shippingDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No shipping exists.";
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

		public async Task<ServiceResponse<ShippingDTO>> GetShippingByIdAsync(Guid id)
		{
			var response = new ServiceResponse<ShippingDTO>();

			var shipping = await _unitOfWork.ShippingRepository.GetByIdAsync(id);
			if (shipping == null)
			{
				response.Success = false;
				response.Message = "Shipping is not existed.";
			}
			else
			{
				response.Success = true;
				response.Message = "Shipping found.";
				response.Data = _mapper.Map<ShippingDTO>(shipping);
			}
			return response;
		}

		public async Task<ServiceResponse<ShippingDTO>> CreateShippingAsync(CUShippingDTO CUshippingDTO)
		{
			var response = new ServiceResponse<ShippingDTO>();
			try
			{
				var shipping = _mapper.Map<Shipping>(CUshippingDTO);

				await _unitOfWork.ShippingRepository.AddAsync(shipping);
				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

				//if (!CUShippingDTO.Shippings.IsNullOrEmpty())
				//{
				//	foreach (var doc in CUShippingDTO.Shippings)
				//	{
				//		if (doc != null)
				//		{
				//			var result = _mapper.Map<Shipping>(doc);
				//			await _unitOfWork.ShippingRepository.AddAsync(result);
				//			await _unitOfWork.SaveChangeAsync();
				//		}
				//	}
				//}

				if (isSuccess)
				{
					var ShippingDTO = _mapper.Map<ShippingDTO>(shipping);
					response.Data = ShippingDTO;
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

		public async Task<ServiceResponse<bool>> DeleteShippingAsync(Guid id)
		{
			var response = new ServiceResponse<bool>();

			var shipping = await _unitOfWork.ShippingRepository.GetByIdAsync(id);
			if (shipping == null)
			{
				response.Success = false;
				response.Message = "Delete fail.";
				return response;
			}
			try
			{
				_unitOfWork.ShippingRepository.SoftRemove(shipping);

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

		public async Task<ServiceResponse<ShippingDTO>> UpdateShippingAsync(Guid id, CUShippingDTO CUshippingDTO)
		{
			var response = new ServiceResponse<ShippingDTO>();
			try
			{
				var shipping = await _unitOfWork.ShippingRepository.GetByIdAsync(id);

				if (shipping == null)
				{
					response.Success = false;
					response.Message = "Shipping is not existed.";
					return response;
				}
				var result = _mapper.Map(CUshippingDTO, shipping);

				_unitOfWork.ShippingRepository.Update(shipping);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Data = _mapper.Map<ShippingDTO>(result);
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
	}
}