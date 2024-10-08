using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Firebase;
using Application.Interfaces.InterfaceServices.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Image
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirebaseStorageService _firebaseStorageService;

        public ImageService(IUnitOfWork unitOfWork, IFirebaseStorageService firebaseStorageService)
        {
            _unitOfWork = unitOfWork;
            _firebaseStorageService = firebaseStorageService;
        }

        public async Task UploadAttachmentImages(List<IFormFile> imageFiles, Guid attachmentId)
        {
            if (imageFiles.IsNullOrEmpty())
            {
                throw new Exception("No Image File found");
            }
            var folderPath = $"Attachment/{attachmentId}";
            var imageUrls = await _firebaseStorageService.UploadImagesAsync(imageFiles, folderPath);
            var images = imageUrls.Select(url => new Domain.Entities.Image { AttachmentId = attachmentId, ImageUrl = url }).ToList();
            await _unitOfWork.ImageRepository.AddRangeAsync(images);
            await _unitOfWork.SaveChangeAsync();
        }

        //public async Task UploadProductImages(List<IFormFile> imageFiles, int productId)
        //{
        //    if (imageFiles.IsNullOrEmpty())
        //    {
        //        throw new Exception("No Image File found");
        //    }
        //    var folderPath = $"product/{productId}";
        //    var imageUrls = await _firebaseStorageService.UploadImagesAsync(imageFiles, folderPath);
        //    var images = imageUrls.Select(url => new Image { ProductId = productId, UrlPath = url }).ToList();
        //    await _unitOfWork.ImageRepository.AddRangeAsync(images);
        //    await _unitOfWork.SaveChangeAsync();
        //}

        public async Task DeleteImages(IEnumerable<Domain.Entities.Image> images)
        {
            var imageUrls = images.Select(p => p.ImageUrl);
            await _firebaseStorageService.DeleteImagesAsync(imageUrls.ToList());
            await _unitOfWork.ImageRepository.DeleteRangeAsync(images);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
