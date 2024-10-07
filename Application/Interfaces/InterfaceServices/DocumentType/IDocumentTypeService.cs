using Application.Commons;
using Application.ViewModels.DocumentTypeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.DocumentType
{
    public interface IDocumentTypeService
    {
        Task<ServiceResponse<IEnumerable<DocumentTypeDTO>>> GetDocumentTypeAsync();
        Task<ServiceResponse<DocumentTypeDTO>> GetDocumentTypeByIdAsync(Guid id);
        Task<ServiceResponse<CUDocumentTypeDTO>> CreateDocumentTypeAsync(CUDocumentTypeDTO cUDocumentTypeDTO);
        Task<ServiceResponse<CUDocumentTypeDTO>> UpdateDocumentTypeAsync(Guid id, CUDocumentTypeDTO cUDocumentTypeDTO);
        Task<ServiceResponse<bool>> DeleteDocumentTypeAsync(Guid id);
    }
}
