using Application.Interfaces.InterfaceServices.Agency;
using Application.ViewModels.AgencyDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Agency
{
    public class AgencyController : BaseController
    {
        private readonly IAgencyService _service;
        public AgencyController(IAgencyService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _service.GetAgencyAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetAgencyByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CUAgencyDTO cUAgencyDTO)
        {
            var result = await _service.CreateAgencyAsync(cUAgencyDTO);
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
        public async Task<IActionResult> Update(Guid id, CUAgencyDTO cUAgencyDTO)
        {
            var result = await _service.UpdateAgencyAsync(id, cUAgencyDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAgencyAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
