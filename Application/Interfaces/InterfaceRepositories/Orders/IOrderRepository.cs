using Domain.Entities;

namespace Application.Interfaces.InterfaceRepositories.Orders
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> GetByPhoneNumberAsync(string num);
    }
}