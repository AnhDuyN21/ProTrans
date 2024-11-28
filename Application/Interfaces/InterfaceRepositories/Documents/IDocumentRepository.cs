using Domain.Entities;

namespace Application.Interfaces.InterfaceRepositories.Documents
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        Task<List<Document>> GetByOrderIdAsync(Guid id);
        Task<bool> UpdateDocumentTranslationStatusByDocumentId(Guid documentId, string status);
        Task<decimal> CaculateDocumentPrice(Guid? firstLanguageId, Guid? secondLanguageId, Guid? documentTypeId, int pageNumber, int numberOfCopies, bool notarizationRequest, Guid? notarizationId, int numberOfNotarizedCopies);

    }
}
