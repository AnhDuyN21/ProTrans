using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.InterfaceServices.Image
{
    public interface IImageService
    {
        Task UploadAttachmentImages(List<IFormFile> imageFiles, Guid attachmentId);
        Task DeleteImages(IEnumerable<Domain.Entities.Image> images);
    }
}
