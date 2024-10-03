using Application.Commons;
using Application.ViewModels.FeedbackDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.Feedbacks
{
	public interface IFeedbackService
	{
		public Task<ServiceResponse<IEnumerable<FeedbackDTO>>> GetAllFeedbacksAsync();
		public Task<ServiceResponse<FeedbackDTO>> GetFeedbackByIdAsync(Guid id);
		//public Task<ServiceResponse<FeedbackDTO>> UpdateFeedbackAsync(Guid id, CUFeedbackDTO feedback);
		public Task<ServiceResponse<FeedbackDTO>> CreateFeedbackAsync(CUFeedbackDTO feedback);
		public Task<ServiceResponse<bool>> DeleteFeedbackAsync(Guid id);
	}
}