using Application.Interfaces.InterfaceServices.Notarization;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.NotarizationDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Notarization
{
    public class NotarizationController : BaseController
    {
        private readonly INotarizationService _notarizationService;
        public NotarizationController(INotarizationService notarizationService) 
        { 
            _notarizationService = notarizationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetNotarizationList()
        {
            var result = await _notarizationService.GetNotarizationAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotarizationById(Guid id)
        {
            var result = await _notarizationService.GetNotarizationByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNotarization(CreateNotarizationDTO createNotarizationDTO)
        {
            var result = await _notarizationService.CreateNotarizationAsync(createNotarizationDTO);
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
        public async Task<IActionResult> UpdateNotarization(Guid id, CreateNotarizationDTO createNotarizationDTO)
        {
            var result = await _notarizationService.UpdateNotarizationAsync(id, createNotarizationDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotarization(Guid id)
        {
            var result = await _notarizationService.DeleteNotarizationAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
