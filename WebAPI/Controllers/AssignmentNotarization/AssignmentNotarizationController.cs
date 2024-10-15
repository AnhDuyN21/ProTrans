using Application.Interfaces.InterfaceServices.AssignmentNotarization;
using Application.ViewModels.AssignmentNotarizationDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.assignmentNotarization
{
    public class AssignmentNotarizationController : BaseController
    {
        private readonly IAssignmentNotarizationService _assignmentNotarizationService;
        public AssignmentNotarizationController(IAssignmentNotarizationService assignmentNotarizationService)
        {
            _assignmentNotarizationService = assignmentNotarizationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssignmentNotarizationList()
        {
            var result = await _assignmentNotarizationService.GetAllAssignmentNotarizationsAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentNotarizationById(Guid id)
        {
            var result = await _assignmentNotarizationService.GetAllAssignmentNotarizationByShipperIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAssignmentNotarization([FromBody] CUAssignmentNotarizationDTO cuAssignmentNotarizationDTO)
        {
            var result = await _assignmentNotarizationService.CreateAssignmentNotarizationAsync(cuAssignmentNotarizationDTO);
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
        public async Task<IActionResult> UpdateAssignmentNotarization(Guid id, [FromBody] CUAssignmentNotarizationDTO cudassignmentNotarizationDTO)
        {
            var result = await _assignmentNotarizationService.UpdateAssignmentNotarizationAsync(id, cudassignmentNotarizationDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignmentNotarization(Guid id)
        {
            var result = await _assignmentNotarizationService.DeleteAssignmentNotarizationAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }


    }
}
