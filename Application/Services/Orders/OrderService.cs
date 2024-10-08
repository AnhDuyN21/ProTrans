﻿using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Orders;
using Application.ViewModels.OrderDTOs;
using AutoMapper;
using Domain.Entities;
using System.Data.Common;

namespace Application.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<OrderDTO>>> GetAllOrdersAsync()
        {
            var response = new ServiceResponse<IEnumerable<OrderDTO>>();

            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllAsync();
                var orderDTOs = _mapper.Map<List<OrderDTO>>(orders);

                if (orderDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Get all successfully.";
                    response.Data = orderDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "No order exists.";
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

        public async Task<ServiceResponse<OrderDTO>> GetOrderByIdAsync(Guid id)
        {
            var response = new ServiceResponse<OrderDTO>();

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                response.Success = false;
                response.Message = "Order is not existed.";
            }
            else
            {
                response.Success = true;
                response.Message = "Order found.";
                response.Data = _mapper.Map<OrderDTO>(order);
            }
            return response;
        }

        public async Task<ServiceResponse<OrderDTO>> GetByPhoneNumberAsync(string num)
        {
            var response = new ServiceResponse<OrderDTO>();

            var order = await _unitOfWork.OrderRepository.GetByPhoneNumberAsync(num);
            if (order == null)
            {
                response.Success = false;
                response.Message = "Order is not existed.";
            }
            else
            {
                response.Success = true;
                response.Message = "Order found.";
                response.Data = _mapper.Map<OrderDTO>(order);
            }
            return response;
        }

        public async Task<ServiceResponse<OrderDTO>> CreateOrderAsync(CUOrderDTO CUorderDTO)
        {
            var response = new ServiceResponse<OrderDTO>();
            try
            {
                var order = _mapper.Map<Order>(CUorderDTO);

                await _unitOfWork.OrderRepository.AddAsync(order);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

                //if (!CUorderDTO.Orders.IsNullOrEmpty())
                //{
                //	foreach (var doc in CUorderDTO.Orders)
                //	{
                //		if (doc != null)
                //		{
                //			var result = _mapper.Map<Order>(doc);
                //			await _unitOfWork.OrderRepository.AddAsync(result);
                //			await _unitOfWork.SaveChangeAsync();
                //		}
                //	}
                //}

                if (isSuccess)
                {
                    var orderDTO = _mapper.Map<OrderDTO>(order);
                    response.Data = orderDTO;
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

        public async Task<ServiceResponse<bool>> DeleteOrderAsync(Guid id)
        {
            var response = new ServiceResponse<bool>();

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                response.Success = false;
                response.Message = "Delete fail.";
                return response;
            }
            try
            {
                _unitOfWork.OrderRepository.SoftRemove(order);

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

        public async Task<ServiceResponse<OrderDTO>> UpdateOrderAsync(Guid id, CUOrderDTO CUorderDTO)
        {
            var response = new ServiceResponse<OrderDTO>();
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);

                if (order == null)
                {
                    response.Success = false;
                    response.Message = "Order is not existed.";
                    return response;
                }
                var result = _mapper.Map(CUorderDTO, order);

                _unitOfWork.OrderRepository.Update(order);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Data = _mapper.Map<OrderDTO>(result);
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