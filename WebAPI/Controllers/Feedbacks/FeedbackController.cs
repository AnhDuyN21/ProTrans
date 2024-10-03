using Application.Interfaces.InterfaceServices.Feedbacks;
using Application.Interfaces.InterfaceServices.Language;
using Application.Services.Language;
using Application.ViewModels.FeedbackDTOs;
using Application.ViewModels.LanguageDTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Feedbacks
{
	public class FeedbackController : BaseController
	{
		private readonly IFeedbackService feedbackService;
		public FeedbackController(IFeedbackService feedbackService)
		{
			this.feedbackService = feedbackService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllFeedbacks()
		{
			var result = await feedbackService.GetAllFeedbacksAsync();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetFeedbackById(Guid id)
		{
			var result = await feedbackService.GetFeedbackByIdAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> CreateFeedback([FromBody] CUFeedbackDTO feedback)
		{
			var result = await feedbackService.CreateFeedbackAsync(feedback);
			if (result.Success)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFeedback(Guid id)
		{
			var result = await feedbackService.DeleteFeedbackAsync(id);
			if (!result.Success)
			{
				return NotFound(result);
			}
			return Ok(result);
		}
	}
}