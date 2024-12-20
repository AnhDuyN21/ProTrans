﻿using Application.Interfaces.InterfaceServices.AssignmentShippings;
using Application.ViewModels.AssignmentShippingDTOs;
using Domain.Enums;
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
			var result = await assignmentShippingService.GetAssignmentShippingsByShipperIdAsync(id);
			return Ok(result);
		}

		[HttpGet("GetPrepareShipByShipperId")]
		public async Task<IActionResult> GetPrepareShipAssignmentShippingsByShipperId(Guid id)
		{
			var result = await assignmentShippingService.GetPrepareShipAssignmentShippingsByShipperIdAsync(id);
			return Ok(result);
		}
		[HttpGet("GetShippingShipByShipperId")]
		public async Task<IActionResult> GetShippingShipAssignmentShippingsByShipperId(Guid id)
		{
			var result = await assignmentShippingService.GetShippingShipAssignmentShippingsByShipperIdAsync(id);
			return Ok(result);
		}

		[HttpGet("GetPickUpByShipperId")]
		public async Task<IActionResult> GetPickUpAssignmentShippingsByShipperId(Guid id)
		{
			var result = await assignmentShippingService.GetPickUpAssignmentShippingsByShipperIdAsync(id);
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

		//[HttpPost]
		//public async Task<IActionResult> CreateAssignmentShipping([FromBody] CreateAssignmentShippingDTO assignmentShipping)
		//{
		//	var result = await assignmentShippingService.CreateAssignmentShippingAsync(assignmentShipping);
		//	if (result.Success)
		//	{
		//		return Ok(result);
		//	}
		//	else
		//	{
		//		return BadRequest(result);
		//	}
		//}

		[HttpPost("Ship")]
		public async Task<IActionResult> CreateAssignmentShippingToShip([FromBody] CreateAssignmentShippingDTO assignmentShipping)
		{
			var result = await assignmentShippingService.CreateAssignmentShippingToShipAsync(assignmentShipping);
			if (result.Success)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPost("PickUp")]
		public async Task<IActionResult> CreateAssignmentShippingToPickUp([FromBody] CreateAssignmentShippingDTO assignmentShipping)
		{
			var result = await assignmentShippingService.CreateAssignmentShippingToPickUpAsync(assignmentShipping);
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

		[HttpPut("UpdateToCompleted")]
		public async Task<IActionResult> UpdateAssignmentShippingToCompleted(Guid id)
		{
			var result = await assignmentShippingService.UpdateAssignmentShippingStatusAsync(id, AssignmentShippingStatus.Completed.ToString());
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpPut("UpdateToShipping")]
		public async Task<IActionResult> UpdateAssignmentShippingToShipping(Guid id)
		{
			var result = await assignmentShippingService.UpdateAssignmentShippingStatusAsync(id, AssignmentShippingStatus.Shipping.ToString());
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
