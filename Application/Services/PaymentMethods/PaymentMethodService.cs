using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.PaymentMethods;
using Application.ViewModels.PaymentMethodDTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PaymentMethods
{
	public class PaymentMethodService : IPaymentMethodService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public PaymentMethodService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<IEnumerable<PaymentMethodDTO>>> GetAllPaymentMethodsAsync()
		{
			var response = new ServiceResponse<IEnumerable<PaymentMethodDTO>>();

			try
			{
				var paymentMethods = await _unitOfWork.PaymenMethodRepository.GetAllAsync();
				var paymentMethodDTOs = _mapper.Map<List<PaymentMethodDTO>>(paymentMethods);

				if (paymentMethodDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get all successfully.";
					response.Data = paymentMethodDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No payment method exists.";
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

		public async Task<ServiceResponse<PaymentMethodDTO>> GetPaymentMethodByIdAsync(Guid id)
		{
			var response = new ServiceResponse<PaymentMethodDTO>();

			var paymentMethod = await _unitOfWork.PaymenMethodRepository.GetByIdAsync(id);
			if (paymentMethod == null)
			{
				response.Success = false;
				response.Message = "Payment method is not existed.";
			}
			else
			{
				response.Success = true;
				response.Message = "Payment method found.";
				response.Data = _mapper.Map<PaymentMethodDTO>(paymentMethod);
			}
			return response;
		}

		public async Task<ServiceResponse<PaymentMethodDTO>> CreatePaymentMethodAsync(CUPaymentMethodDTO CUpaymentMethodDTO)
		{
			var response = new ServiceResponse<PaymentMethodDTO>();
			try
			{
				var paymentMethod = _mapper.Map<PaymentMethod>(CUpaymentMethodDTO);

				await _unitOfWork.PaymenMethodRepository.AddAsync(paymentMethod);
				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

				if (isSuccess)
				{
					var paymentMethodDTO = _mapper.Map<PaymentMethodDTO>(paymentMethod);
					response.Data = paymentMethodDTO;
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

		public async Task<ServiceResponse<bool>> DeletePaymentMethodAsync(Guid id)
		{
			var response = new ServiceResponse<bool>();

			var paymentMethod = await _unitOfWork.PaymenMethodRepository.GetByIdAsync(id);
			if (paymentMethod == null)
			{
				response.Success = false;
				response.Message = "Delete fail.";
				return response;
			}
			try
			{
				_unitOfWork.PaymenMethodRepository.SoftRemove(paymentMethod);

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

		public async Task<ServiceResponse<PaymentMethodDTO>> UpdatePaymentMethodAsync(Guid id, CUPaymentMethodDTO CUpaymentMethodDTO)
		{
			var response = new ServiceResponse<PaymentMethodDTO>();
			try
			{
				var paymentMethod = await _unitOfWork.PaymenMethodRepository.GetByIdAsync(id);

				if (paymentMethod == null)
				{
					response.Success = false;
					response.Message = "Payment method is not existed.";
					return response;
				}
				var result = _mapper.Map(CUpaymentMethodDTO, paymentMethod);

				_unitOfWork.PaymenMethodRepository.Update(paymentMethod);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Data = _mapper.Map<PaymentMethodDTO>(result);
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