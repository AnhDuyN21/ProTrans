using Domain.Entities;

namespace Application.Interfaces.InterfaceRepositories.Shippings
{
    public interface IShippingRepository : IGenericRepository<AssignmentShipping>
    {
        Task<List<AssignmentShipping>> GetByShipperIdAsync(Guid id);
    }
}
