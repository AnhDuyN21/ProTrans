namespace Application.Interfaces.InterfaceRepositories.Request
{
    public interface IRequestRepository : IGenericRepository<Domain.Entities.Request>
    {
        Guid GetCurrentCustomerId();
    }
}
