using Application.Commons;
using Application.ViewModels.DocumentDTOs;

namespace Application.Interfaces.InterfaceServices.Documents
{
    public interface IDocumentService
    {
        public Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetAllDocumentsAsync();
        public Task<ServiceResponse<DocumentDTO>> GetDocumentByIdAsync(Guid id);
        public Task<ServiceResponse<DocumentDTO>> UpdateDocumentAsync(Guid id, CUDocumentDTO document);
        public Task<ServiceResponse<DocumentDTO>> CreateDocumentAsync(CUDocumentDTO document);
        public Task<ServiceResponse<bool>> DeleteDocumentAsync(Guid id);
        public Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetDocumentsByOrderIdAsync(Guid id);
	}
}