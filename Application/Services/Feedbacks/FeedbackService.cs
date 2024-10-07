using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Feedbacks;
using Application.ViewModels.FeedbackDTOs;
using AutoMapper;
using Domain.Entities;
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

		public async Task<ServiceResponse<FeedbackDTO>> CreateFeedbackAsync(CUFeedbackDTO CUfeedbackDTO)
		{
			var response = new ServiceResponse<FeedbackDTO>();
			try
			{
				var feedback = _mapper.Map<Feedback>(CUfeedbackDTO);

				await _unitOfWork.FeedbackRepository.AddAsync(feedback);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					var feedbackDTO = _mapper.Map<FeedbackDTO>(feedback);
					response.Data = feedbackDTO;
					response.Success = true;
					response.Message = "Create successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error saving.";
				}
			}
			catch (DbException ex)
			{
				response.Success = false;
				response.Message = "Database error.";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { ex.Message };
			}

			return response;
		}

		public async Task<ServiceResponse<bool>> DeleteFeedbackAsync(Guid id)
		{
			var response = new ServiceResponse<bool>();

			var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(id);
			if (feedback == null)
			{
				response.Success = false;
				response.Message = "Delete fail.";
				return response;
			}
			try
			{
				_unitOfWork.FeedbackRepository.SoftRemove(feedback);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Success = true;
					response.Message = "Delete successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error deleting.";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error.";
				response.ErrorMessages = new List<string> { ex.Message };
			}

			return response;
		}
	}
}