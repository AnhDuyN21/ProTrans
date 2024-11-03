using Application.Interfaces.InterfaceServices.AssignmentShippings;
using Application.ViewModels.AssignmentShippingDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.AssignmentShippings
{
	public class AssignmentShippingController : BaseController
	{
		private readonly IAssignmentShippingService assignmentShippingService;
		public AssignmentShippingController(IAssignmentShippingService assignmentShippingService)
		{
			this.assignmentShippingService = assignmentShippingService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAssignmentShippings()
		{
			var result = await assignmentShippingService.GetAllAssignmentShippingsAsync();
			return Ok(result);
		}

		[HttpGet("GetByShipperId")]
		public async Task<IActionResult> GetAssignmentShippingsByShipperId(Guid id)
		{
			var result = await assignmentShippingService.GetAssignmentShippingByIdAsync(id);
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAssignmentShippingById(Guid id)
		{
			var result = await assignmentShippingService.GetAssignmentShippingByIdAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAssignmentShipping([FromBody] CreateAssignmentShippingDTO assignmentShipping)
		{
			var result = await assignmentShippingService.CreateAssignmentShippingAsync(assignmentShipping);
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
		public async Task<IActionResult> UpdateAssignmentShipping(Guid id, [FromBody] UpdateAssignmentShippingDTO CUassignmentShippingDTO)
		{
			var result = await assignmentShippingService.UpdateAssignmentShippingAsync(id, CUassignmentShippingDTO);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAssignmentShipping(Guid id)
		{
			var result = await assignmentShippingService.DeleteAssignmentShippingAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
	}
}
