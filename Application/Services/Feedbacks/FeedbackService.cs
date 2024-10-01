using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Feedbacks;
using Application.ViewModels.FeedbackDTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Feedbacks
{
	public class FeedbackService : IFeedbackService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<IEnumerable<FeedbackDTO>>> GetAllFeedbacksAsync()
		{
			var response = new ServiceResponse<IEnumerable<FeedbackDTO>>();

			try
			{
				var feedbacks = await _unitOfWork.FeedbackRepository.GetAllAsync();
				var feedbackDTOs = _mapper.Map<List<FeedbackDTO>>(feedbacks);

				if (feedbackDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get all successfully.";
					response.Data = feedbackDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No feedback exists.";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
			}
			return response;
		}

		public async Task<ServiceResponse<FeedbackDTO>> GetFeedbackByIdAsync(Guid id)
		{
			var response = new ServiceResponse<FeedbackDTO>();

			var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(id);
			if (feedback == null)
			{
				response.Success = false;
				response.Message = "Feedback is not existed.";
			}
			else
			{
				response.Success = true;
				response.Message = "Feedback found.";
				response.Data = _mapper.Map<FeedbackDTO>(feedback);
			}
			return response;
		}
	}
}