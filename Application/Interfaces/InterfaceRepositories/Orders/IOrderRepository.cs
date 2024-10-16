using Domain.Entities;

namespace Application.Interfaces.InterfaceRepositories.Orders
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
		Task<List<Order>> GetByPhoneNumberAsync(string num);
		Guid GetCurrentStaffId();
	}
}
