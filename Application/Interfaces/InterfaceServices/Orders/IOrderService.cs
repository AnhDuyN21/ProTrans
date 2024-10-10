using Application.Commons;
using Application.ViewModels.OrderDTOs;

namespace Application.Interfaces.InterfaceServices.Orders
{
    public interface IOrderService
    {
        public Task<ServiceResponse<IEnumerable<OrderDTO>>> GetAllOrdersAsync();
        public Task<ServiceResponse<OrderDTO>> GetOrderByIdAsync(Guid id);
        public Task<ServiceResponse<OrderDTO>> UpdateOrderAsync(Guid id, CUOrderDTO order);
        public Task<ServiceResponse<OrderDTO>> CreateOrderAsync(CUOrderDTO order);
        public Task<ServiceResponse<bool>> DeleteOrderAsync(Guid id);
        public Task<ServiceResponse<OrderDTO>> GetByPhoneNumberAsync(string num);
    }
}