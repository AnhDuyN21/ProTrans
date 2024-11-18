using Application.Commons;
using Application.ViewModels.OrderDTOs;

namespace Application.Interfaces.InterfaceServices.Orders
{
	public interface IOrderService
	{
		public Task<ServiceResponse<IEnumerable<OrderDTO>>> GetAllOrdersAsync();
		public Task<ServiceResponse<IEnumerable<OrderDTO>>> GetCompletedOrdersAsync();
		public Task<ServiceResponse<IEnumerable<OrderDTO>>> GetOfflineOrdersAsync();
		public Task<ServiceResponse<IEnumerable<OrderDTO>>> GetOnlineOrdersAsync();
		public Task<ServiceResponse<IEnumerable<OrderDTO>>> GetOrdersToPickUpAsync();
		public Task<ServiceResponse<IEnumerable<OrderDTO>>> GetCompletedOrdersByAgencyIdAsync(Guid id);
		public Task<ServiceResponse<OrderDTO>> GetOrderByIdAsync(Guid id);
		public Task<ServiceResponse<OrderDTO>> UpdateOrderAsync(Guid id, UpdateOrderDTO order);
		public Task<ServiceResponse<OrderDTO>> CreateOrderAsync(CreateOrderDTO order);
		public Task<ServiceResponse<OrderDTO>> CreateOrderFromRequestAsync(Guid requestId);
		public Task<ServiceResponse<bool>> DeleteOrderAsync(Guid id, string reason);
		public Task<ServiceResponse<IEnumerable<OrderDTO>>> GetOrdersByPhoneNumberAsync(string num);
		Task<ServiceResponse<OrderDTO>> UpdateOrderStatusAsync(Guid id, string status);

    }
}
