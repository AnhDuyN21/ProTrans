using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.AssignmentShippings;
using Application.Interfaces.InterfaceServices.Documents;
using Application.ViewModels.AssignmentShippingDTOs;
using Application.ViewModels.TransactionDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MailKit.Search;
using Org.BouncyCastle.Crypto.Agreement.Srp;
using System.Data.Common;

namespace Application.Services.AssignmentShippings
{
	public class AssignmentShippingService : IAssignmentShippingService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IDocumentService _documentService;
		public AssignmentShippingService(IUnitOfWork unitOfWork, IMapper mapper, IDocumentService documentService)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_documentService = documentService;
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
					response.Message = "Not exist.";
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
					response.Message = "Not exist.";
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

		public async Task<ServiceResponse<IEnumerable<AssignmentShippingDTO>>> GetPrepareShipAssignmentShippingsByShipperIdAsync(Guid id)
		{
			var response = new ServiceResponse<IEnumerable<AssignmentShippingDTO>>();

			try
			{
				var assignmentShippings = await _unitOfWork.AssignmentShippingRepository.GetAllAsync(x => x.ShipperId == id && x.Type == "Ship" && x.Status.Equals("Preparing"));
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
					response.Message = "Not exist.";
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
		public async Task<ServiceResponse<IEnumerable<AssignmentShippingDTO>>> GetShippingShipAssignmentShippingsByShipperIdAsync(Guid id)
		{
			var response = new ServiceResponse<IEnumerable<AssignmentShippingDTO>>();

			try
			{
				var assignmentShippings = await _unitOfWork.AssignmentShippingRepository.GetAllAsync(x => x.ShipperId == id && x.Type == "Ship" && x.Status.Equals("Shipping"));
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
					response.Message = "Not exist.";
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

		public async Task<ServiceResponse<IEnumerable<AssignmentShippingDTO>>> GetPickUpAssignmentShippingsByShipperIdAsync(Guid id)
		{
			var response = new ServiceResponse<IEnumerable<AssignmentShippingDTO>>();

			try
			{
				var assignmentShippings = await _unitOfWork.AssignmentShippingRepository.GetAllAsync(x => x.ShipperId == id && x.Type == "PickUp" && x.Status != "Completed");
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
					response.Message = "Not exist.";
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

		public async Task<ServiceResponse<AssignmentShippingDTO>> CreateAssignmentShippingToShipAsync(CreateAssignmentShippingDTO CUassignmentShippingDTO)
		{
			var response = new ServiceResponse<AssignmentShippingDTO>();
			try
			{
				var assignmentShipping = _mapper.Map<AssignmentShipping>(CUassignmentShippingDTO);
				assignmentShipping.Status = "Preparing";
				assignmentShipping.Type = "Ship";

				await _unitOfWork.AssignmentShippingRepository.AddAsync(assignmentShipping);

				var documents = await _documentService.GetDocumentsByOrderIdAsync(CUassignmentShippingDTO.OrderId);
				var order = await _unitOfWork.OrderRepository.GetByIdAsync(CUassignmentShippingDTO.OrderId);
				if (order != null)
				{
					order.Status = OrderStatus.Delivering.ToString();
					_unitOfWork.OrderRepository.Update(order);
				}
				if (documents != null)
				{
					var imageShipping = new ImageShipping
					{
						AssignmentShippingId = assignmentShipping.Id
					};
					await _unitOfWork.ImageShippingRepository.AddAsync(imageShipping);
				}

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
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

		public async Task<ServiceResponse<AssignmentShippingDTO>> CreateAssignmentShippingToPickUpAsync(CreateAssignmentShippingDTO CUassignmentShippingDTO)
		{
			var response = new ServiceResponse<AssignmentShippingDTO>();
			try
			{
				var assignmentShipping = _mapper.Map<AssignmentShipping>(CUassignmentShippingDTO);
				assignmentShipping.Status = "Preparing";
				assignmentShipping.Type = "PickUp";

				await _unitOfWork.AssignmentShippingRepository.AddAsync(assignmentShipping);

				var documents = await _unitOfWork.DocumentRepository.GetByOrderIdAsync(CUassignmentShippingDTO.OrderId);
				if (documents != null)
				{
					foreach (var doc in documents)
					{
						if (doc.NotarizationRequest)
						{
							var imageShipping = new ImageShipping
							{
								DocumentId = doc.Id,
								AssignmentShippingId = assignmentShipping.Id
							};
							doc.NotarizationStatus = DocumentNotarizationStatus.PickingUp.ToString();
							_unitOfWork.DocumentRepository.Update(doc);
							await _unitOfWork.ImageShippingRepository.AddAsync(imageShipping);
						}
					}
				}

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
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
				response.Message = "Not exist.";
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
					response.Message = "Not exist.";
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
		public async Task<ServiceResponse<AssignmentShippingDTO>> UpdateAssignmentShippingStatusAsync(Guid id, string status)
		{
			var response = new ServiceResponse<AssignmentShippingDTO>();
			try
			{
				var assignmentShipping = await _unitOfWork.AssignmentShippingRepository.GetByIdAsync(id);
				if (assignmentShipping == null)
				{
					response.Success = false;
					response.Message = "Not exist.";
				}
				else
				{
					assignmentShipping.Status = status;
					_unitOfWork.AssignmentShippingRepository.Update(assignmentShipping);
					if (assignmentShipping.Type == AssignmentShippingType.Ship.ToString())
					{
						var order = await _unitOfWork.OrderRepository.GetByIdAsync(assignmentShipping.OrderId);
						if (order != null)
						{
							if (status == AssignmentShippingStatus.Completed.ToString())
							{
								order.Status = OrderStatus.Delivered.ToString();
								var account = await _unitOfWork.AccountRepository.GetAsync(x => x.PhoneNumber.Equals(order.PhoneNumber));
								if (account != null)
								{
									TransactionDTO transactionDTO = new TransactionDTO();
									transactionDTO.AccountId = account.Id;
									transactionDTO.OrderId = order.Id;
									var transaction = _mapper.Map<Transaction>(transactionDTO);
									await _unitOfWork.TransactionRepository.AddAsync(transaction);
								}
							}
							if (status == AssignmentShippingStatus.Shipping.ToString()) order.Status = OrderStatus.Delivering.ToString();
							_unitOfWork.OrderRepository.Update(order);
						}
					}
					if (assignmentShipping.Type == AssignmentShippingType.PickUp.ToString())
					{
						var documents = await _unitOfWork.DocumentRepository.GetAllAsync(x => x.OrderId == assignmentShipping.OrderId && x.NotarizationRequest);
						if (status == AssignmentShippingStatus.Completed.ToString())
							foreach (var doc in documents)
							{
								doc.NotarizationStatus = DocumentNotarizationStatus.PickedUp.ToString();
								_unitOfWork.DocumentRepository.Update(doc);
							}
						if (status == AssignmentShippingStatus.Shipping.ToString())
							foreach (var doc in documents)
							{
								doc.NotarizationStatus = DocumentNotarizationStatus.PickingUp.ToString();
								_unitOfWork.DocumentRepository.Update(doc);
							}
					}

					var result = await _unitOfWork.SaveChangeAsync() > 0;
					if (result)
					{
						response.Success = true;
						response.Message = "Update successfully.";
						response.Data = _mapper.Map<AssignmentShippingDTO>(assignmentShipping);
					}
					else
					{
						response.Success = false;
						response.Message = "Update fail.";
					}
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
