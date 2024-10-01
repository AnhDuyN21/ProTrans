using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.Firebase
{
    public interface IFirebaseStorageService
    {
        Task<string> UploadImageAsync(IFormFile imageFile, string imagePath);

        string GetImageUrl(string folderName, string imageName);

        Task<string> UpdateImageAsync(IFormFile imageFile, string imagePath);

        Task DeleteImageAsync(string imageName);

        Task DeleteImagesAsync(List<string> imageUrls);

        Task<string[]> UploadImagesAsync(List<IFormFile> imageFiles, string folderPath);
    }
}
