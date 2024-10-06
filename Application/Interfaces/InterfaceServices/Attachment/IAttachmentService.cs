using Application.Commons;
using Application.ViewModels.AttachmentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.Attachment
{
    public interface IAttachmentService
    {
        Task<ServiceResponse<IEnumerable<AttachmentDTO>>> GetAttachmentAsync();
        Task<ServiceResponse<AttachmentDTO>> GetAttachmentByIdAsync(Guid id);
        Task<ServiceResponse<AttachmentDTO>> CreateAttachmentAsync(CreateAttachmentDTO createAttachmentDTO);
        Task<ServiceResponse<AttachmentDTO>> UpdateAttachmentAsync(Guid id, CreateAttachmentDTO createAttachmentDTO);
        Task<ServiceResponse<bool>> DeleteAttachmentAsync(Guid id);
    }
}
