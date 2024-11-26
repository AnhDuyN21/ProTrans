using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Orders;
using Application.ViewModels.OrderDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.Common;
using System.Net.WebSockets;

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
					response.Message = "No orders exist.";
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

		public async Task<ServiceResponse<IEnumerable<OrderDTO>>> GetOfflineOrdersAsync()
		{
			var response = new ServiceResponse<IEnumerable<OrderDTO>>();

			try
			{
				var orders = await _unitOfWork.OrderRepository.GetAllAsync(x => x.RequestId == null);
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
					response.Message = "No orders exist.";
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

		public async Task<ServiceResponse<IEnumerable<OrderDTO>>> GetOrdersToPickUpAsync()
		{
			var response = new ServiceResponse<IEnumerable<OrderDTO>>();
			var targetOrders = new List<Order>();
			try
			{
				var orders = await _unitOfWork.OrderRepository.GetAllAsync(x => x.Status.Equals("Processing") || x.Status.Equals("Implementing"));

				if (orders == null)
				{
					response.Success = true;
					response.Message = "No orders exist.";
				}
				else
				{
					foreach (var order in orders)
					{
						var documents = await _unitOfWork.DocumentRepository.GetByOrderIdAsync(order.Id);
						if (documents != null)
						{
							foreach (var doc in documents)
							{
								if (doc.NotarizationRequest && doc.NotarizationStatus == DocumentNotarizationStatus.Processing.ToString())
								{
									targetOrders.Add(order);
									break;
								}
							}
						}
					}
					var orderDTOs = _mapper.Map<List<OrderDTO>>(targetOrders);
					if (orderDTOs.Count != 0)
					{
						response.Success = true;
						response.Message = "Get successfully.";
						response.Data = orderDTOs;
					}
					else
					{
						response.Success = true;
						response.Message = "No orders exist.";
					}
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

		public async Task<ServiceResponse<IEnumerable<OrderDTO>>> GetOnlineOrdersAsync()
		{
			var response = new ServiceResponse<IEnumerable<OrderDTO>>();

			try
			{
				var orders = await _unitOfWork.OrderRepository.GetAllAsync(x => x.RequestId != null);
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
					response.Message = "No orders exist.";
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

		public async Task<ServiceResponse<IEnumerable<OrderDTO>>> GetCompletedOrdersAsync()
		{
			var response = new ServiceResponse<IEnumerable<OrderDTO>>();

			try
			{
				var orders = await _unitOfWork.OrderRepository.GetAllAsync();
				var completedOrders = orders.Where(order => order.ShipRequest && order.Status == "Completed").ToList();
				var orderDTOs = _mapper.Map<List<OrderDTO>>(completedOrders);

				if (orderDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get completed orders successfully.";
					response.Data = orderDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No completed orders exist.";
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

		public async Task<ServiceResponse<IEnumerable<OrderDTO>>> GetCompletedOrdersByAgencyIdAsync(Guid id)
		{
			var response = new ServiceResponse<IEnumerable<OrderDTO>>();

			try
			{
				var orders = await _unitOfWork.OrderRepository.GetAllAsync();
				var targetOrders = orders.Where(order => order.ShipRequest && order.Status == "Completed" && order.AgencyId == id).ToList();
				var orderDTOs = _mapper.Map<List<OrderDTO>>(targetOrders);

				if (orderDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get orders successfully.";
					response.Data = orderDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No orders exist.";
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

		public async Task<ServiceResponse<IEnumerable<OrderDTO>>> GetOrdersByPhoneNumberAsync(string num)
		{
			var response = new ServiceResponse<IEnumerable<OrderDTO>>();

			try
			{
				var orders = await _unitOfWork.OrderRepository.GetByPhoneNumberAsync(num);
				var orderDTOs = _mapper.Map<List<OrderDTO>>(orders);

				if (orderDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get successfully.";
					response.Data = orderDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No orders exist.";
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

		public async Task<ServiceResponse<OrderDTO>> CreateOrderAsync(CreateOrderDTO CUorderDTO)
		{
			var response = new ServiceResponse<OrderDTO>();
			try
			{
				var order = _mapper.Map<Order>(CUorderDTO);
				order.OrderCode = order.Id.ToString().Substring(0, 6).ToUpper();
				order.TotalPrice = 0;

				if (CUorderDTO.Documents != null)
				{
					foreach (var doc in CUorderDTO.Documents)
					{
						var quotePrice = await _unitOfWork.QuotePriceRepository.GetQuotePriceBy2LanguageId(doc.FirstLanguageId, doc.SecondLanguageId);
						if (quotePrice == null)
						{
							response.Success = false;
							response.Message = "Có ít nhất 1 cặp ngôn ngữ không được hỗ trợ.";
							return response;
						}
						var documentType = await _unitOfWork.DocumentTypeRepository.GetByIdAsync(doc.DocumentTypeId);
						if (quotePrice.PricePerPage != null && documentType != null)
						{
							order.TotalPrice += quotePrice.PricePerPage.Value * doc.PageNumber * documentType.PriceFactor;
						}
						order.TotalPrice += (doc.NumberOfCopies - 1) * (doc.PageNumber * 500 + 10000);
						if (doc.NotarizationRequest)
						{
							var notarization = await _unitOfWork.NotarizationRepository.GetByIdAsync((Guid)doc.NotarizationId);
							if (notarization != null)
							{
								order.TotalPrice += notarization.Price * doc.NumberOfNotarizedCopies;
							}
						}
					}
				}

				//if (order.Deadline != DateTime.MinValue) order.Deadline = order.Deadline.Value.ToUniversalTime();
				var staffId = _unitOfWork.OrderRepository.GetCurrentStaffId();
				order.CreatedBy = staffId;
				var staff = await _unitOfWork.AccountRepository.GetByIdAsync(staffId);
				order.AgencyId = (staff != null) ? staff.AgencyId : null;
				order.Status = "Processing";

				await _unitOfWork.OrderRepository.AddAsync(order);
				if (order.Documents != null)
				{
					foreach (var doc in order.Documents)
					{
						if (doc.FileType == "Hard") doc.Code = doc.Id.ToString().Substring(0, 6).ToUpper();
						if (doc.NotarizationRequest) doc.NotarizationStatus = "PickedUp";
						else doc.NotarizationStatus = "None";
						doc.TranslationStatus = "Processing";
					}
				}

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

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

		public async Task<ServiceResponse<OrderDTO>> CreateOrderFromRequestAsync(Guid requestId)
		{
			var response = new ServiceResponse<OrderDTO>();
			try
			{
				var order = new Order();
				var request = await _unitOfWork.RequestRepository.GetByIdAsync(requestId);
				if (request != null)
				{
					if (request.CustomerId != null)
					{
						var customer = await _unitOfWork.AccountRepository.GetByIdAsync(request.CustomerId.Value);
						if (customer != null)
						{
							order.FullName = customer.FullName;
							order.PhoneNumber = customer.PhoneNumber;
							order.Address = customer.Address;
						}
					}
					order.ShipRequest = request.ShipRequest;
					order.Deadline = request.Deadline;
					order.TotalPrice = request.EstimatedPrice;
					order.Status = OrderStatus.Processing.ToString();
					var staffId = _unitOfWork.OrderRepository.GetCurrentStaffId();
					var staff = await _unitOfWork.AccountRepository.GetByIdAsync(staffId);
					order.AgencyId = (staff != null) ? staff.AgencyId : null;
					order.RequestId = requestId;
					await _unitOfWork.OrderRepository.AddAsync(order);
					var documents = await _unitOfWork.DocumentRepository.GetAllAsync(x => x.RequestId == requestId);
					if (documents != null)
					{
						foreach (var doc in documents)
						{
							doc.OrderId = order.Id;
						}
					}
					request.Status = RequestStatus.Finish.ToString();
					_unitOfWork.RequestRepository.Update(request);
				}

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
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

		public async Task<ServiceResponse<bool>> DeleteOrderAsync(Guid id, string reason)
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
				order.Reason = reason;
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

		public async Task<ServiceResponse<OrderDTO>> UpdateOrderAsync(Guid id, UpdateOrderDTO CUorderDTO)
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

				var properties = typeof(UpdateOrderDTO).GetProperties();
				foreach (var property in properties)
				{
					var newValue = property.GetValue(CUorderDTO);
					var oldValue = typeof(Order).GetProperty(property.Name)?.GetValue(order);

					if (newValue == null)
					{
						typeof(UpdateOrderDTO).GetProperty(property.Name)?.SetValue(CUorderDTO, oldValue);
					}
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
		public async Task<ServiceResponse<OrderDTO>> UpdateOrderStatusAsync(Guid id, string status)
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
				order.Status = status;
				_unitOfWork.OrderRepository.Update(order);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Data = _mapper.Map<OrderDTO>(order);
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
