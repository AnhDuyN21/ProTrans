using Domain.Entities;

namespace Application.Interfaces.InterfaceRepositories.Documents
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        Task<List<Document>> GetByOrderIdAsync(Guid id);
    }
}
