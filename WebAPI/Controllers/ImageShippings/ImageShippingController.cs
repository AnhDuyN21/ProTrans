using Application.Interfaces.InterfaceServices.ImageShippings;
using Application.ViewModels.ImageShippingDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.ImageShippings
{
	public class ImageShippingController : BaseController
	{
		private readonly IImageShippingService imageShippingService;
		public ImageShippingController(IImageShippingService imageShippingService)
		{
			this.imageShippingService = imageShippingService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllImageShippings()
		{
			var result = await imageShippingService.GetAllImageShippingsAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetImageShippingById(Guid id)
		{
			var result = await imageShippingService.GetImageShippingByIdAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpGet("GetByAssignmentShippingId")]
		public async Task<IActionResult> GetImageShippingsByAssignmentShippingId(Guid id)
		{
			var result = await imageShippingService.GetImageShippingsByAssignmentShippingIdAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateImageShipping([FromBody] CreateImageShippingDTO imageShipping)
		{
			var result = await imageShippingService.CreateImageShippingAsync(imageShipping);
			if (result.Success)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateImageShipping(Guid id, [FromBody] UpdateImageShippingDTO updateImageShipping)
		{
			var result = await imageShippingService.UpdateImageShippingAsync(id, updateImageShipping);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpPut("UpdateImage")]
		public async Task<IActionResult> UpdateImage(Guid id, string urlPath)
		{
			var result = await imageShippingService.UpdateImageAsync(id, urlPath);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteImageShipping(Guid id)
		{
			var result = await imageShippingService.DeleteImageShippingAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
	}
}
