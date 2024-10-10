using Application.Commons;
using Application.ViewModels.FeedbackDTOs;

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