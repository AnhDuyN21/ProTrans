using Application.Interfaces.InterfaceServices.AssignmentTranslation;
using Application.ViewModels.AssignmentTranslationDTOs;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.assignmentTranslation
{
	public class AssignmentTranslationController : BaseController
	{
		private readonly IAssignmentTranslationService _assignmentTranslationService;
		public AssignmentTranslationController(IAssignmentTranslationService assignmentTranslationService)
		{
			_assignmentTranslationService = assignmentTranslationService;
		}

		[HttpGet]
		public async Task<IActionResult> GetassignmentTranslationList()
		{
			var result = await _assignmentTranslationService.GetAllAssignmentTranslationsAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetassignmentTranslationById(Guid id)
		{
			var result = await _assignmentTranslationService.GetAllAssignmentTranslationByTranslatorIdAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
		[HttpGet("GetTranslating")]
		public async Task<IActionResult> GetTranslatingassignmentTranslationById(Guid id)
		{
			var result = await _assignmentTranslationService.GetAssignmentTranslationByTranslatorIdAndStatusAsync(id, "Translating");
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
		[HttpGet("GetTranslated")]
		public async Task<IActionResult> GetTranslatedassignmentTranslationById(Guid id)
		{
			var result = await _assignmentTranslationService.GetAssignmentTranslationByTranslatorIdAndStatusAsync(id, "Translated");
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
		[HttpPost]
		public async Task<IActionResult> CreateassignmentTranslation([FromBody] CUAssignmentTranslationDTO cuAssignmentTranslationDTO)
		{
			var result = await _assignmentTranslationService.CreateAssignmentTranslationAsync(cuAssignmentTranslationDTO);
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
		public async Task<IActionResult> UpdateassignmentTranslation(Guid id, [FromBody] CUAssignmentTranslationDTO cudassignmentTranslationDTO)
		{
			var result = await _assignmentTranslationService.UpdateAssignmentTranslationAsync(id, cudassignmentTranslationDTO);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteassignmentTranslation(Guid id)
		{
			var result = await _assignmentTranslationService.DeleteAssignmentTranslationAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
		//[HttpPut("Translating")]
		//public async Task<IActionResult> UpdateStatusAssignmentTranslation1(Guid id)
		//{
		//    var result = await _assignmentTranslationService.UpdateStatusAssignmentTranslationAsync(id, AssignmentTranslationStatus.Translating);
		//    if (!result.Success)
		//    {
		//        return NotFound(result);
		//    }
		//    return Ok(result);
		//}
		[HttpPut("Complete")]
		public async Task<IActionResult> UpdateStatusAssignmentTranslation2(Guid id, string? urlPath)
		{
			var result = await _assignmentTranslationService.UpdateStatusAssignmentTranslationAsync(id, urlPath);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

	}
}
