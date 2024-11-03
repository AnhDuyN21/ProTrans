using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.AssignmentShippings;
using Application.ViewModels.AssignmentShippingDTOs;
using AutoMapper;
using Domain.Entities;
using System.Data.Common;

namespace Application.Services.AssignmentShippings
{
	public class AssignmentShippingService : IAssignmentShippingService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public AssignmentShippingService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<IEnumerable<AssignmentShippingDTO>>> GetAllAssignmentShippingsAsync()
		{
			var response = new ServiceResponse<IEnumerable<AssignmentShippingDTO>>();

			try
			{
				var assignmentShippings = await _unitOfWork.AssignmentShippingRepository.GetAllAsync();
				var assignmentShippingDTOs = _mapper.Map<List<AssignmentShippingDTO>>(assignmentShippings);

				if (assignmentShippingDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get all successfully.";
					response.Data = assignmentShippingDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No assignment shipping exists.";
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

		public async Task<ServiceResponse<IEnumerable<AssignmentShippingDTO>>> GetAssignmentShippingsByShipperIdAsync(Guid id)
		{
			var response = new ServiceResponse<IEnumerable<AssignmentShippingDTO>>();

			try
			{
				var assignmentShippings = await _unitOfWork.AssignmentShippingRepository.GetByShipperIdAsync(id);
				var assignmentShippingDTOs = _mapper.Map<List<AssignmentShippingDTO>>(assignmentShippings);

				if (assignmentShippingDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "Get successfully.";
					response.Data = assignmentShippingDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "No shipping exists.";
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

		public async Task<ServiceResponse<AssignmentShippingDTO>> GetAssignmentShippingByIdAsync(Guid id)
		{
			var response = new ServiceResponse<AssignmentShippingDTO>();

			var assignmentShipping = await _unitOfWork.AssignmentShippingRepository.GetByIdAsync(id);
			if (assignmentShipping == null)
			{
				response.Success = false;
				response.Message = "Assignment shipping is not existed.";
			}
			else
			{
				response.Success = true;
				response.Message = "Assignment shipping found.";
				response.Data = _mapper.Map<AssignmentShippingDTO>(assignmentShipping);
			}
			return response;
		}

		public async Task<ServiceResponse<AssignmentShippingDTO>> CreateAssignmentShippingAsync(CreateAssignmentShippingDTO CUassignmentShippingDTO)
		{
			var response = new ServiceResponse<AssignmentShippingDTO>();
			try
			{
				var assignmentShipping = _mapper.Map<AssignmentShipping>(CUassignmentShippingDTO);
				assignmentShipping.Status = "Preparing";

				await _unitOfWork.AssignmentShippingRepository.AddAsync(assignmentShipping);
				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

				//if (!CUShippingDTO.Shippings.IsNullOrEmpty())
				//{
				//	foreach (var doc in CUShippingDTO.Shippings)
				//	{
				//		if (doc != null)
				//		{
				//			var result = _mapper.Map<Shipping>(doc);
				//			await _unitOfWork.ShippingRepository.AddAsync(result);
				//			await _unitOfWork.SaveChangeAsync();
				//		}
				//	}
				//}

				if (isSuccess)
				{
					var assignmentShippingDTO = _mapper.Map<AssignmentShippingDTO>(assignmentShipping);
					response.Data = assignmentShippingDTO;
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

		public async Task<ServiceResponse<bool>> DeleteAssignmentShippingAsync(Guid id)
		{
			var response = new ServiceResponse<bool>();

			var assignmentShipping = await _unitOfWork.AssignmentShippingRepository.GetByIdAsync(id);
			if (assignmentShipping == null)
			{
				response.Success = false;
				response.Message = "Delete fail.";
				return response;
			}
			try
			{
				_unitOfWork.AssignmentShippingRepository.SoftRemove(assignmentShipping);

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

		public async Task<ServiceResponse<AssignmentShippingDTO>> UpdateAssignmentShippingAsync(Guid id, UpdateAssignmentShippingDTO CUassignmentShippingDTO)
		{
			var response = new ServiceResponse<AssignmentShippingDTO>();
			try
			{
				var assignmentShipping = await _unitOfWork.AssignmentShippingRepository.GetByIdAsync(id);

				if (assignmentShipping == null)
				{
					response.Success = false;
					response.Message = "Shipping is not existed.";
					return response;
				}

				var properties = typeof(UpdateAssignmentShippingDTO).GetProperties();
				foreach (var property in properties)
				{
					var newValue = property.GetValue(CUassignmentShippingDTO);
					var oldValue = typeof(AssignmentShipping).GetProperty(property.Name)?.GetValue(assignmentShipping);

					if (newValue == null)
					{
						typeof(UpdateAssignmentShippingDTO).GetProperty(property.Name)?.SetValue(CUassignmentShippingDTO, oldValue);
					}
				}

				var result = _mapper.Map(CUassignmentShippingDTO, assignmentShipping);

				_unitOfWork.AssignmentShippingRepository.Update(assignmentShipping);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Data = _mapper.Map<AssignmentShippingDTO>(result);
					response.Success = true;
					response.Message = "Update successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error updating.";
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
