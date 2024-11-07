using Application.Commons;
using Application.ViewModels.ImageShippingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.ImageShippings
{
	public interface IImageShippingService
	{
		public Task<ServiceResponse<IEnumerable<ImageShippingDTO>>> GetAllImageShippingsAsync();
		public Task<ServiceResponse<ImageShippingDTO>> GetImageShippingByIdAsync(Guid id);
		public Task<ServiceResponse<ImageShippingDTO>> UpdateImageShippingAsync(Guid id, UpdateImageShippingDTO imageShipping);
		public Task<ServiceResponse<ImageShippingDTO>> CreateImageShippingAsync(CreateImageShippingDTO imageShipping);
		public Task<ServiceResponse<bool>> DeleteImageShippingAsync(Guid id);
		public Task<ServiceResponse<bool>> UpdateImageAsync(Guid id, string urlPath);
	}
}
