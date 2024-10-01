using Application.Interfaces.InterfaceServices.Feedbacks;
using Application.Interfaces.InterfaceServices.Language;
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
	}
}