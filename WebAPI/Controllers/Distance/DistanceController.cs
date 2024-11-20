using Application.Interfaces.InterfaceServices.Agency;
using Application.Interfaces.InterfaceServices.Distance;
using Application.ViewModels.AgencyDTOs;
using Application.ViewModels.DistanceDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Distance
{

    public class DistanceController : BaseController
    {
        private readonly IDistanceService _service;
        public DistanceController(IDistanceService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _service.GetAsync();
            return Ok(result);
        }
        [HttpGet("{distanceId}")]
        public async Task<IActionResult> GetById(Guid distanceId)
        {
            var result = await _service.GetByIdAsync(distanceId);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUpdateDistanceDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPut("{distanceId}")]
        public async Task<IActionResult> Update(Guid distanceId, CreateUpdateDistanceDTO dto)
        {
            var result = await _service.UpdateAsync(distanceId, dto);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("{distanceId}")]
        public async Task<IActionResult> Delete(Guid distanceId)
        {
            var result = await _service.DeleteAsync(distanceId);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
