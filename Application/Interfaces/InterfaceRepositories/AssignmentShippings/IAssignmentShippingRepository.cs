using Domain.Entities;

namespace Application.Interfaces.InterfaceRepositories.IAssignmentShippings
{
    public interface IAssignmentShippingRepository : IGenericRepository<AssignmentShipping>
    {
        Task<List<AssignmentShipping>> GetByShipperIdAsync(Guid id);
    }
}
