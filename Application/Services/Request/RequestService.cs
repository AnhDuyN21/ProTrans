﻿using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Request;
using Application.ViewModels.DocumentDTOs;
using Application.ViewModels.OrderDTOs;
using Application.ViewModels.RequestDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Google.Apis.Storage.v1.Data;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Data.Common;
using static Google.Apis.Requests.BatchRequest;

namespace Application.Services.Request
{
	public class RequestService : IRequestService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IClaimsService _claimsService;
		private readonly ICurrentTime _currentTime;
		public RequestService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService, ICurrentTime currentTime)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_claimsService = claimsService;
			_currentTime = currentTime;
		}
		public async Task<ServiceResponse<IEnumerable<RequestDTO>>> GetRequestAsync()
		{
			var response = new ServiceResponse<IEnumerable<RequestDTO>>();

			try
			{
				var requestList = await _unitOfWork.RequestRepository.GetAllAsync();
				var requestDTOs = _mapper.Map<List<RequestDTO>>(requestList);
                var sortedRequestDTOs = requestDTOs.OrderByDescending(r => r.CreatedDate).ToList();

                if (sortedRequestDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "List retrieved successfully";
					response.Data = sortedRequestDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "Not have data in list";
				}

			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
			}

			return response;
		}

		public async Task<ServiceResponse<IEnumerable<RequestToQuoteDTO>>> GetRequestsToQuoteAsync()
		{
			var response = new ServiceResponse<IEnumerable<RequestToQuoteDTO>>();
			try
			{
				var requests = await _unitOfWork.RequestRepository.GetAllAsync();
				var requestDTOs = _mapper.Map<List<RequestToQuoteDTO>>(requests);
				foreach (var request in requestDTOs)
				{
					var documents = await _unitOfWork.DocumentRepository.GetAllAsync(x => x.RequestId == request.Id);
					request.Documents = _mapper.Map<List<DocumentDTO>>(documents);
					var customer = await _unitOfWork.AccountRepository.GetAsync(x => x.Id == request.CustomerId);
					if (customer != null)
					{
						request.FullName = customer.FullName;
						request.PhoneNumber = customer.PhoneNumber;
						request.Email = customer.Email;
						request.Address = customer.Address;
					}
				}
                var sortedRequestDTOs = requestDTOs.OrderByDescending(r => r.CreatedDate).ToList();
                if (sortedRequestDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "List retrieved successfully.";
					response.Data = sortedRequestDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "Not have data in list.";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
			}
			return response;
		}

		public async Task<ServiceResponse<IEnumerable<RequestDTO>>> GetRequestByCustomerAsync(Guid customerId)
		{
			var response = new ServiceResponse<IEnumerable<RequestDTO>>();
			try
			{
				var requests = await _unitOfWork.RequestRepository.GetAllAsync(x => x.CustomerId == customerId);
				var requestDTOs = _mapper.Map<List<RequestDTO>>(requests);

				foreach (var request in requestDTOs)
				{
					var documents = await _unitOfWork.DocumentRepository.GetAllAsync(x => x.RequestId == request.Id);
					if (documents != null)
					{
						var documentDTOs = _mapper.Map<List<DocumentDTO>>(documents);
						foreach (var document in documentDTOs)
						{
							var documentPrice = await _unitOfWork.DocumentPriceRepository.GetAsync(x => x.DocumentId == document.Id);
							if (documentPrice != null)
							{
								var documentPriceDTO = _mapper.Map<DocumentPriceDTO>(documentPrice);
								document.DocumentPrice = documentPriceDTO;
							}
							var documentHistories = await _unitOfWork.DocumentHistoryRepository.GetAllAsync(x => x.DocumentId == document.Id);
							if (documentHistories != null)
							{
								document.ChangedDocument = new ChangedDocumentDTO();
								document.ChangedDocument.DocumentId = document.Id;
								var documentHistoryDTOs = _mapper.Map<List<DocumentHistoryDTO>>(documentHistories);
								foreach (var documentHistory in documentHistoryDTOs)
								{
									if (documentHistory.Name == "PageNumber")
									{
										document.ChangedDocument.OldPageNumber = documentHistory.OldValue;
									}
									if (documentHistory.Name == "DocumentTypeId")
									{
										document.ChangedDocument.OldDocumentTypeId = documentHistory.OldValue;
									}
									if (documentHistory.Name == "NotarizationId")
									{
										document.ChangedDocument.OldNotarizationId = documentHistory.OldValue;
									}
								}
								document.DocumentHistory = documentHistoryDTOs;
							}
						}
						request.Documents = documentDTOs;
					}
				}
                var sortedRequestDTOs = requestDTOs.OrderByDescending(r => r.CreatedDate).ToList();
                if (sortedRequestDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "List retrieved successfully.";
					response.Data = sortedRequestDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "Not have data in list.";
				}

			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
			}
			return response;
		}

		public async Task<ServiceResponse<IEnumerable<RequestCustomerDTO>>> GetRequestWithStatusAsync(string status)
		{
			var response = new ServiceResponse<IEnumerable<RequestCustomerDTO>>();

			try
			{
				var requestList = await _unitOfWork.RequestRepository.GetAllAsync(x => x.Status == status);
				var requestDTOs = new List<RequestCustomerDTO>();
				foreach (var request in requestList)
				{
					var customer = await _unitOfWork.AccountRepository.GetAsync(x => x.Id == request.CustomerId);
					if (customer == null)
					{
						response.Success = false;
						response.Message = "request không có thông tin khách hàng";
						return response;
					}
					var requestDTO = new RequestCustomerDTO
					{
						Id = request.Id,
						Deadline = request.Deadline,
						EstimatedPrice = request.EstimatedPrice,
						Status = request.Status,
						IsConfirmed = request.IsConfirmed,
						PickUpRequest = request.PickUpRequest,
						ShipRequest = request.ShipRequest,
						IsDeleted = (bool)request.IsDeleted,
						CustomerId = request.CustomerId,
						FullName = customer.FullName,
						PhoneNumber = customer.PhoneNumber,
						Email = customer.Email,
						Address = customer.Address
					};
					requestDTOs.Add(requestDTO);
				}
                var sortedRequestDTOs = requestDTOs.OrderByDescending(r => r.CreatedDate).ToList();

                if (sortedRequestDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "List retrieved successfully";
					response.Data = sortedRequestDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "Not have data in list";
				}

			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
			}

			return response;
		}
		public async Task<ServiceResponse<IEnumerable<RequestCustomerDTO>>> GetRequestWithStatusQuotedAsync(Guid customerId)
		{
			var response = new ServiceResponse<IEnumerable<RequestCustomerDTO>>();

			try
			{
				var requestList = await _unitOfWork.RequestRepository.GetAllAsync(x => x.Status == "Quoted");
				var requestDTOs = new List<RequestCustomerDTO>();
				foreach (var request in requestList)
				{
					if (request.CustomerId == customerId)
					{
						var customer = await _unitOfWork.AccountRepository.GetAsync(x => x.Id == request.CustomerId);
						if (customer == null)
						{
							response.Success = false;
							response.Message = "request không có thông tin khách hàng";
							return response;
						}
						var requestDTO = new RequestCustomerDTO
						{
							Id = request.Id,
							Deadline = request.Deadline,
							EstimatedPrice = request.EstimatedPrice,
							Status = request.Status,
							IsConfirmed = request.IsConfirmed,
							PickUpRequest = request.PickUpRequest,
							ShipRequest = request.ShipRequest,
							IsDeleted = (bool)request.IsDeleted,
							CustomerId = request.CustomerId,
							FullName = customer.FullName,
							PhoneNumber = customer.PhoneNumber,
							Email = customer.Email,
							Address = customer.Address
						};
						requestDTOs.Add(requestDTO);
					}

				}
                var sortedRequestDTOs = requestDTOs.OrderByDescending(r => r.CreatedDate).ToList();

                if (sortedRequestDTOs.Count != 0)
				{
					response.Success = true;
					response.Message = "List retrieved successfully";
					response.Data = sortedRequestDTOs;
				}
				else
				{
					response.Success = true;
					response.Message = "Not have data in list";
				}

			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
			}

			return response;
		}


		public async Task<ServiceResponse<RequestDTO>> GetRequestByIdAsync(Guid id)
		{
			var response = new ServiceResponse<RequestDTO>();

			var requestGetById = await _unitOfWork.RequestRepository.GetByIdAsync(id);
			if (requestGetById == null)
			{
				response.Success = false;
				response.Message = "Id is not existed";
			}
			else
			{
				response.Success = true;
				response.Message = "Request found";
				response.Data = _mapper.Map<RequestDTO>(requestGetById);
			}
			return response;
		}
		public async Task<ServiceResponse<CreateRequestDTO>> CreateRequestAsync(CreateRequestDTO createRequestDTO)
		{
			var response = new ServiceResponse<CreateRequestDTO>();
			try
			{
				var customerId = _unitOfWork.RequestRepository.GetCurrentCustomerId();
				decimal price = 0;

				if (createRequestDTO.Documents == null)
				{
					response.Success = false;
					response.Message = "Tài liệu không được để trống.";
					return response;
				}
				var request = new Domain.Entities.Request
				{
					CustomerId = customerId,
					ShipRequest = createRequestDTO.ShipRequest,
					PickUpRequest = createRequestDTO.PickUpRequest,
					Deadline = createRequestDTO.Deadline
				};
				await _unitOfWork.RequestRepository.AddAsync(request);

				var requestId = request.Id;

				var priceDetails = new List<(Guid DocumentId, decimal TranslationPrice, decimal NotarizationPrice)>();

				var documents = _mapper.Map<List<Domain.Entities.Document>>(createRequestDTO.Documents);

				foreach (var doc in documents)
				{
                    doc.Code = doc.Id.ToString().Substring(0, 6).ToUpper();
                    if (doc.NumberOfCopies < doc.NumberOfNotarizedCopies)
					{
                        response.Success = false;
                        response.Message = "Số bản cần dịch phải nhiều hơn số bản công chứng.";
                        return response;
                    }
					var quotePrice = await _unitOfWork.QuotePriceRepository.GetQuotePriceBy2LanguageId((Guid)doc.FirstLanguageId, (Guid)doc.SecondLanguageId);
					if (quotePrice == null)
					{
						var firstLanguage = await _unitOfWork.LanguageRepository.GetByIdAsync((Guid)doc.FirstLanguageId);
						var secondLanguage = await _unitOfWork.LanguageRepository.GetByIdAsync((Guid)doc.SecondLanguageId);
						response.Success = false;
						response.Message = $"Chúng tôi không hỗ trợ dịch từ {firstLanguage.Name} sang {secondLanguage.Name}.";
						return response;
					}
					var translationPrice = await _unitOfWork.DocumentRepository.CaculateDocumentTranslationPrice(doc.FirstLanguageId,
																										   doc.SecondLanguageId,
																										   doc.DocumentTypeId,
																										   doc.PageNumber,
																										   doc.NumberOfCopies);
					var notarizationPrice = await _unitOfWork.DocumentRepository.CaculateDocumentNotarizationPrice(doc.NotarizationRequest,
																										 doc.NotarizationId,
																										 doc.NumberOfNotarizedCopies);

					price += translationPrice + notarizationPrice;
					doc.RequestId = requestId;
					doc.FileType = FileType.Soft.ToString();
					//Cập nhật document status
					doc.TranslationStatus = DocumentTranslationStatus.Waiting.ToString();
					doc.NotarizationStatus = doc.NotarizationRequest ? DocumentNotarizationStatus.Waiting.ToString() : DocumentNotarizationStatus.None.ToString();

					priceDetails.Add((doc.Id, translationPrice, notarizationPrice));
				}
				await _unitOfWork.DocumentRepository.AddRangeAsync(documents);
				await _unitOfWork.SaveChangeAsync();
				//Thêm giá của Document vào bảng Document Price
				foreach (var detail in priceDetails)
				{
					var documentPrice = new DocumentPrice
					{
						DocumentId = detail.DocumentId,
						TranslationPrice = detail.TranslationPrice,
						NotarizationPrice = detail.NotarizationPrice,
						Price = detail.TranslationPrice + detail.NotarizationPrice
					};
					await _unitOfWork.DocumentPriceRepository.AddAsync(documentPrice);
				}
				
				request.EstimatedPrice = price;
				//Giá ship cho request
				decimal pickUpPrice = 40000;
				decimal shipPrice = 40000;
				if (request.PickUpRequest == true)
				{
					request.EstimatedPrice += pickUpPrice;
				}
				if (request.ShipRequest == true)
				{
					request.EstimatedPrice += shipPrice;
				}
				request.Status = RequestStatus.Waitting.ToString();
				_unitOfWork.RequestRepository.Update(request);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Success = true;
					response.Message = "Created successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error saving data.";
				}
			}
			catch (DbException ex)
			{
				response.Success = false;
				response.Message = "Database error occurred.";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}
		public async Task<ServiceResponse<RequestDTO>> UpdateRequestAsync(Guid id, UpdateRequestDTO updateRequestDTO)
		{
			var response = new ServiceResponse<RequestDTO>();
			try
			{
				var getRequestById = await _unitOfWork.RequestRepository.GetAsync(x => x.Id == id, includeProperties: "Documents");
				if (getRequestById == null)
				{
					response.Success = false;
					response.Message = "Id not exist!";
					return response;
				}
				if (getRequestById.Status != RequestStatus.Waitting.ToString())
				{
					response.Success = false;
					response.Message = "Trạng thái ban đầu của request phải là Wating. Không thể chỉnh sửa";
					return response;
				}
				var priceDetails = new List<(Guid DocumentId, decimal TranslationPrice, decimal NotarizationPrice)>();
				decimal price = 0;
				foreach (var document in updateRequestDTO.Documents)
				{
					var getById = await _unitOfWork.DocumentRepository.GetByIdAsync(document.Id);
					if (getById == null)
					{
						response.Success = false;
						response.Message = $"Document có Id {getById} không tồn tại";
						return response;
					}

					//thêm document history
					var properties = typeof(UpdateDocumentDTO).GetProperties();
					var documentHistoryList = new List<DocumentHistory>();
					foreach (var property in properties)
					{
						var newValue = typeof(UpdateDocumentFromRequestDTO).GetProperty(property.Name)?.GetValue(document);
						var oldValue = typeof(Document).GetProperty(property.Name)?.GetValue(getById);

						if (!Equals(newValue, oldValue) && newValue != null)
						{
							documentHistoryList.Add(new DocumentHistory
							{
								DocumentId = document.Id,
								Name = property.Name,
								oldValue = oldValue?.ToString(),
							});
						}

						if (newValue == null)
						{
							typeof(UpdateDocumentFromRequestDTO).GetProperty(property.Name)?.SetValue(document, oldValue);
						}
					}
					if (documentHistoryList.Any())
					{
						foreach (var documentHistory in documentHistoryList)
						{
							await _unitOfWork.DocumentHistoryRepository.AddAsync(documentHistory);
							var isAddSuccess = await _unitOfWork.SaveChangeAsync() > 0;
							if (!isAddSuccess)
							{
								response.Success = false;
								response.Message = "Tạo document history không thành công";
								return response;
							}
						}
					}

					var updatedDocument = _mapper.Map(document, getById);
					//Cập nhật document status
					document.TranslationStatus = DocumentTranslationStatus.Waiting.ToString();
					document.NotarizationStatus = (bool)document.NotarizationRequest ? DocumentNotarizationStatus.Waiting.ToString() : DocumentNotarizationStatus.None.ToString();
					updatedDocument.RequestId = id;
					var translationPrice = await _unitOfWork.DocumentRepository.CaculateDocumentTranslationPrice(document.FirstLanguageId,
																										   document.SecondLanguageId,
																										   document.DocumentTypeId,
																										   (int)document.PageNumber,
																										   (int)document.NumberOfCopies);
					var notarizationPrice = await _unitOfWork.DocumentRepository.CaculateDocumentNotarizationPrice((bool)document.NotarizationRequest,
																										 document.NotarizationId,
																										 (int)document.NumberOfNotarizedCopies);

					price += translationPrice + notarizationPrice;
					priceDetails.Add((getById.Id, translationPrice, notarizationPrice));

					_unitOfWork.DocumentRepository.Update(updatedDocument);
					var isUpdateSuccess = await _unitOfWork.SaveChangeAsync() > 0;
					if (!isUpdateSuccess)
					{
						response.Success = false;
						response.Message = $"Update document có id {document.Id} không thành công";
						return response;
					}
					//Cập nhật giá của Document vào bảng Document Price
					var documentPrice = await _unitOfWork.DocumentPriceRepository.GetAsync(x => x.DocumentId == getById.Id);
					if (documentPrice == null)
					{
						response.Success = false;
						response.Message = $"Không tìm thấy Document Price với Document id là: {getById.Id}";
						return response;
					}
					documentPrice.TranslationPrice = translationPrice;
					documentPrice.NotarizationPrice = notarizationPrice;
					documentPrice.Price = translationPrice + notarizationPrice;
					_unitOfWork.DocumentPriceRepository.Update(documentPrice);
					await _unitOfWork.SaveChangeAsync();

				}
				getRequestById.Deadline = updateRequestDTO.Deadline;
				getRequestById.Status = updateRequestDTO.Status;
				if (updateRequestDTO.Documents.Count > 0) getRequestById.EstimatedPrice = price;

				//Giá ship cho request
				decimal pickUpPrice = 40000;
				decimal shipPrice = 40000;
				if (getRequestById.PickUpRequest == true)
				{
					getRequestById.EstimatedPrice += pickUpPrice;
				}
				if (getRequestById.ShipRequest == true)
				{
					getRequestById.EstimatedPrice += shipPrice;
				}
				_unitOfWork.RequestRepository.Update(getRequestById);
				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					var result = _mapper.Map<RequestDTO>(getRequestById);
					response.Data = result;
					response.Success = true;
					response.Message = "Updated successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error saving data.";
				}
			}
			catch (DbException ex)
			{
				response.Success = false;
				response.Message = "Database error occurred.";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}
		public async Task<ServiceResponse<RequestDTO>> UpdateRequestByCustomerAsync(Guid id, CustomerUpdateRequestDTO customerUpdateRequestDTO)
		{
			var response = new ServiceResponse<RequestDTO>();
			try
			{
				var getRequestById = await _unitOfWork.RequestRepository.GetByIdAsync(id);
				if (getRequestById == null)
				{
					response.Success = false;
					response.Message = "Id not exist!";
					return response;
				}
				if (getRequestById.Status != RequestStatus.Quoted.ToString())
				{
					response.Success = false;
					response.Message = "Request chưa được báo giá bởi staff! Cập nhật thất bại";
					return response;
				}
				var updated = _mapper.Map(customerUpdateRequestDTO, getRequestById);
				if (customerUpdateRequestDTO.Status == "Accept")
				{
					updated.IsConfirmed = true;
				}
				else
				{
					updated.IsConfirmed = false;
				}
				_unitOfWork.RequestRepository.Update(updated);
				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					var result = _mapper.Map<RequestDTO>(updated);
					response.Data = result;
					response.Success = true;
					response.Message = "Updated successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Error saving data.";
				}
			}
			catch (DbException ex)
			{
				response.Success = false;
				response.Message = "Database error occurred.";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { ex.Message };
			}
			return response;
		}
		public async Task<ServiceResponse<bool>> DeleteRequestAsync(Guid id)
		{
			var response = new ServiceResponse<bool>();

			var requestGetById = await _unitOfWork.RequestRepository.GetByIdAsync(id);
			if (requestGetById == null)
			{
				response.Success = false;
				response.Message = "Id is not exist";
				return response;
			}

			try
			{
				_unitOfWork.RequestRepository.SoftRemove(requestGetById);

				var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
				if (isSuccess)
				{
					response.Success = true;
					response.Message = "Deleted successfully.";
				}
				else
				{
					response.Success = false;
					response.Message = "Delete fail.";
				}
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Message = "Error";
				response.ErrorMessages = new List<string> { ex.Message };
			}

			return response;
		}
	}
}
