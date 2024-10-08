using Application.Interfaces.InterfaceServices.Request;
using Application.ViewModels.DocumentTypeDTOs;
using Application.ViewModels.RequestDTOs;
using Microsoft.AspNetCore.Http;
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CreateRequestDTO createRequestDTO)
        {
            var result = await _service.UpdateRequestAsync(id, createRequestDTO);
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
