using Application.Commons;
using Application.ViewModels.AttachmentDTOs;

namespace Application.Interfaces.InterfaceServices.Attachment
{
    public interface IAttachmentService
    {
        Task<ServiceResponse<IEnumerable<AttachmentDTO>>> GetAttachmentAsync();
        Task<ServiceResponse<AttachmentDTO>> GetAttachmentByIdAsync(Guid id);
        Task<ServiceResponse<AttachmentDTO>> CreateAttachmentAsync(Guid requestId, CreateAttachmentDTO createAttachmentDTO);
        Task<ServiceResponse<AttachmentDTO>> UpdateAttachmentAsync(Guid id, CreateAttachmentDTO createAttachmentDTO);
        Task<ServiceResponse<bool>> DeleteAttachmentAsync(Guid id);
    }
}
