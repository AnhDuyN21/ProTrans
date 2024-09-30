using Application.Interfaces.InterfaceServices.QuotePrice;
using Application.ViewModels.QuotePriceDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.QuotePrice
{
    public class QuotePriceController : BaseController
    {
        private readonly IQuotePriceService _quotePriceService;
        public QuotePriceController(IQuotePriceService quotePriceService)
        {
            _quotePriceService = quotePriceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetQuotePriceList()
        {
            var result = await _quotePriceService.GetAllQuotePricesAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuotePriceById(Guid id)
        {
            var result = await _quotePriceService.GetQuotePriceByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuotePrice([FromBody] CUQuotePriceDTO createdQuotePriceDTO)
        {
            var result = await _quotePriceService.CreateQuotePriceAsync(createdQuotePriceDTO);
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
        public async Task<IActionResult> UpdateQuotePrice(Guid id, [FromBody] CUQuotePriceDTO cudquotePriceDTO)
        {
            var result = await _quotePriceService.UpdateQuotePriceAsync(id, cudquotePriceDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotePrice(Guid id)
        {
            var result = await _quotePriceService.DeleteQuotePriceAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }


    }
}
