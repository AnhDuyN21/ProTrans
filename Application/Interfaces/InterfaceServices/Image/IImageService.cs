using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.Image
{
    public interface IImageService
    {
        Task UploadAttachmentImages(List<IFormFile> imageFiles, Guid attachmentId);
        Task DeleteImages(IEnumerable<Domain.Entities.Image> images);
    }
}
