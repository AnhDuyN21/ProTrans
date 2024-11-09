using Application.Interfaces.InterfaceServices.Request;
using Application.ViewModels.RequestDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Request
{
    public class RequestController : BaseController
    {
        private readonly IRequestService _service;
        public RequestController(IRequestService requestService)
        {
            _service = requestService;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _service.GetRequestAsync();
            return Ok(result);
        }

        [HttpGet("GetStatusWaitting")]
        public async Task<IActionResult> GetStatusWaitting()
        {
            var result = await _service.GetRequestWithStatusAsync("Waitting");
            return Ok(result);
        }
        [HttpGet("GetStatusQuoted")]
        public async Task<IActionResult> GetStatusQuoted(Guid customerId)
        {
            var result = await _service.GetRequestWithStatusQuotedAsync(customerId);
            return Ok(result);
        }
        [HttpGet("GetStatusAccept")]
        public async Task<IActionResult> GetStatusAccept()
        {
            var result = await _service.GetRequestWithStatusAsync("Accept");
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetRequestByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("GetByCustomerId")]
        public async Task<IActionResult> GetByCustomerId(Guid customerId)
        {
            var result = await _service.GetRequestByCustomerAsync(customerId);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestDTO createRequestDTO)
        {
            var result = await _service.CreateRequestAsync(createRequestDTO);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPut("StaffUpdate")]
        public async Task<IActionResult> Update(Guid requestId, UpdateRequestDTO updateRequestDTO)
        {
            var result = await _service.UpdateRequestAsync(requestId, updateRequestDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPut("CustomerUpdate")]
        public async Task<IActionResult> UpdateByCustomer(Guid requestId, CustomerUpdateRequestDTO customerUpdateRequestDTO)
        {
            var result = await _service.UpdateRequestByCustomerAsync(requestId, customerUpdateRequestDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteRequestAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
