using Application.Interfaces.InterfaceServices.Shippings;
using Application.ViewModels.ShippingDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Shippings
{
    public class ShippingController : BaseController
    {
        private readonly IShippingService shippingService;
        public ShippingController(IShippingService shippingService)
        {
            this.shippingService = shippingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShippings()
        {
            var result = await shippingService.GetAllShippingsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShippingById(Guid id)
        {
            var result = await shippingService.GetShippingByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipping([FromBody] CUShippingDTO shipping)
        {
            var result = await shippingService.CreateShippingAsync(shipping);
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
        public async Task<IActionResult> UpdateShipping(Guid id, [FromBody] CUShippingDTO CUshippingDTO)
        {
            var result = await shippingService.UpdateShippingAsync(id, CUshippingDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipping(Guid id)
        {
            var result = await shippingService.DeleteShippingAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}