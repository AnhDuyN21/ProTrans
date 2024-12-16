using Application.Commons;
using Application.ViewModels.DocumentDTOs;

namespace Application.Interfaces.InterfaceServices.Documents
{
	public interface IDocumentService
	{
		//Document
		public Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetAllDocumentsAsync();
		public Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetDocumentsToBeNotarizedByAgencyIdAsync(Guid id);
		public Task<ServiceResponse<DocumentDTO>> GetDocumentByIdAsync(Guid id);
		public Task<ServiceResponse<DocumentDTO>> UpdateDocumentAsync(Guid id, UpdateDocumentDTO document);
		public Task<ServiceResponse<DocumentDTO>> CreateDocumentAsync(CreateDocumentDTO document);
		public Task<ServiceResponse<bool>> DeleteDocumentAsync(Guid id);
		public Task<ServiceResponse<IEnumerable<DocumentGetByOrderIdDTO>>> GetDocumentsByOrderIdAsync(Guid id);
		public Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetDocumentsByRequestIdAsync(Guid id);
		public Task<ServiceResponse<IEnumerable<DocumentDTO>>> GetDocumentsToBeNotarizedByOrderIdAsync(Guid id);
		//DocumentHistory
		Task<ServiceResponse<IEnumerable<DocumentHistoryDTO>>> GetDocumentHistoryByDocumentIdAsync(Guid documentId);
		Task<ServiceResponse<DocumentHistoryDTO>> GetDocumentHistoryByIdAsync(Guid id);
		//DocumentPrice
		Task<ServiceResponse<DocumentPriceDTO>> GetDocumentPriceByDocumentId(Guid documentId);
		Task<ServiceResponse<CreateDocumentPriceDTO>> CreateDocumentPriceAsync(CreateDocumentPriceDTO createDocumentPriceDTO);
		Task<ServiceResponse<UpdateDocumentPriceDTO>> UpdateDocumentPriceAsync(Guid documentId, UpdateDocumentPriceDTO updateDocumentPriceDTO);
	}
}
