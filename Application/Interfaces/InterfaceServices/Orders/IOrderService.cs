using Application.Commons;
using Application.ViewModels.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.Orders
{
	public interface IOrderService
	{
		public Task<ServiceResponse<IEnumerable<OrderDTO>>> GetAllOrdersAsync();
		public Task<ServiceResponse<OrderDTO>> GetOrderByIdAsync(Guid id);
		public Task<ServiceResponse<OrderDTO>> UpdateOrderAsync(Guid id, CUOrderDTO order);
		public Task<ServiceResponse<OrderDTO>> CreateOrderAsync(CUOrderDTO order);
		public Task<ServiceResponse<bool>> DeleteOrderAsync(Guid id);
	}
}