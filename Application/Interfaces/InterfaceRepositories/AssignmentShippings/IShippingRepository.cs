using Domain.Entities;

namespace Application.Interfaces.InterfaceRepositories.IAssignmentShippings
{
    public interface IShippingRepository : IGenericRepository<AssignmentShipping>
    {
        Task<List<AssignmentShipping>> GetByShipperIdAsync(Guid id);
    }
}
