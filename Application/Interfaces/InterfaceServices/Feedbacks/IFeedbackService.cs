using Application.Commons;
using Application.ViewModels.FeedbackDTOs;
using Application.ViewModels.LanguageDTOs;
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
		//public Task<ServiceResponse<LanguageDTO>> UpdateFeedbackAsync(Guid id, CULanguageDTO cudLanguageDTO);
		//public Task<ServiceResponse<LanguageDTO>> CreateFeedbackAsync(CULanguageDTO languageDTO);
		//public Task<ServiceResponse<bool>> DeleteFeedbackAsync(Guid id);
	}
}