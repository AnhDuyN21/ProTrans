using Application.Commons;
using Application.ViewModels.DocumentDTOs;

namespace Application.Interfaces.InterfaceServices.Documents
{
	public interface IDocumentService
	{
		public Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetAllDocumentsAsync();
		public Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetDocumentsToBeNotarizedAsync();
		public Task<ServiceResponse<DocumentDTO>> GetDocumentByIdAsync(Guid id);
		public Task<ServiceResponse<DocumentDTO>> UpdateDocumentAsync(Guid id, UpdateDocumentDTO document);
		public Task<ServiceResponse<DocumentDTO>> CreateDocumentAsync(CreateDocumentDTO document);
		public Task<ServiceResponse<bool>> DeleteDocumentAsync(Guid id);
		public Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetDocumentsByOrderIdAsync(Guid id);
		public Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetDocumentsToBeNotarizedByOrderIdAsync(Guid id);
		Task<ServiceResponse<IEnumerable<DocumentHistoryDTO>>> GetDocumentHistoryByDocumentIdAsync(Guid documentId);

    }
}