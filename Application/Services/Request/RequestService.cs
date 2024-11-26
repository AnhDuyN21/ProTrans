using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Request;
using Application.ViewModels.DocumentDTOs;
using Application.ViewModels.OrderDTOs;
using Application.ViewModels.RequestDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Google.Apis.Storage.v1.Data;
using System.Data.Common;

namespace Application.Services.Request
{
    public class RequestService : IRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimsService _claimsService;
        public RequestService(IUnitOfWork unitOfWork, IMapper mapper, IClaimsService claimsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimsService = claimsService;
        }
        public async Task<ServiceResponse<IEnumerable<RequestDTO>>> GetRequestAsync()
        {
            var response = new ServiceResponse<IEnumerable<RequestDTO>>();

            try
            {
                var requestList = await _unitOfWork.RequestRepository.GetAllAsync();
                var requestDTOs = _mapper.Map<List<RequestDTO>>(requestList);

                if (requestDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "List retrieved successfully";
                    response.Data = requestDTOs;
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
        public async Task<ServiceResponse<IEnumerable<RequestDTO>>> GetRequestByCustomerAsync(Guid customerId)
        {
            var response = new ServiceResponse<IEnumerable<RequestDTO>>();

            try
            {
                var requestList = await _unitOfWork.RequestRepository.GetAllAsync(x => x.CustomerId == customerId);
                var requestDTOs = _mapper.Map<List<RequestDTO>>(requestList);

                if (requestDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "List retrieved successfully";
                    response.Data = requestDTOs;
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

                if (requestDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "List retrieved successfully";
                    response.Data = requestDTOs;
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

                if (requestDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "List retrieved successfully";
                    response.Data = requestDTOs;
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

                    foreach (var doc in createRequestDTO.Documents)
                    {
                        var quotePrice = await _unitOfWork.QuotePriceRepository.GetQuotePriceBy2LanguageId(doc.FirstLanguageId, doc.SecondLanguageId);
                        if (quotePrice == null)
                        {
                            response.Success = false;
                            response.Message = "Có ít nhất 1 cặp ngôn ngữ không được hỗ trợ.";
                            return response;
                        }
                        var documentType = await _unitOfWork.DocumentTypeRepository.GetByIdAsync(doc.DocumentTypeId);
                        if (quotePrice.PricePerPage != null && documentType != null)
                        {
                            price += quotePrice.PricePerPage.Value * doc.PageNumber * documentType.PriceFactor;
                        }
                        price += (doc.NumberOfCopies - 1) * (doc.PageNumber * 500 + 10000);
                        if (doc.NotarizationRequest)
                        {
                            var notarization = await _unitOfWork.NotarizationRepository.GetByIdAsync((Guid)doc.NotarizationId);
                            if (notarization != null)
                            {
                                price += notarization.Price * doc.NumberOfNotarizedCopies;
                            }
                        }
                        doc.FileType = FileType.Soft.ToString();
                        doc.TranslationStatus = DocumentTranslationStatus.Waiting.ToString();
                        if (doc.NotarizationRequest == true)
                        {
                            doc.NotarizationStatus = DocumentNotarizationStatus.Waiting.ToString();
                        }
                        else
                        {
                            doc.NotarizationStatus = DocumentNotarizationStatus.None.ToString();
                        }
                    }
                
                var request = _mapper.Map<Domain.Entities.Request>(createRequestDTO);
                request.CustomerId = customerId;
                request.EstimatedPrice = price;

                request.Status = RequestStatus.Waitting.ToString();
                await _unitOfWork.RequestRepository.AddAsync(request);
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
        public async Task<ServiceResponse<UpdateRequestDTO>> UpdateRequestAsync(Guid id, UpdateRequestDTO updateRequestDTO)
        {
            var response = new ServiceResponse<UpdateRequestDTO>();
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
                if (updateRequestDTO.Documents == null || updateRequestDTO.Documents.Count == 0)
                {
                    response.Success = false;
                    response.Message = "Danh sách tài liệu trống!";
                    return response;
                }
                //update Document
                foreach ( var document in updateRequestDTO.Documents )
                {
                    var getById = await _unitOfWork.DocumentRepository.GetByIdAsync(document.Id);
                    if(getById == null)
                    {
                        response.Success = false;
                        response.Message = $"Document có Id {getById} không tồn tại";
                        return response;
                    }

                    //add document history
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
                    updatedDocument.RequestId = id;
                    _unitOfWork.DocumentRepository.Update(updatedDocument);
                    var isUpdateSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                    if (!isUpdateSuccess)
                    {
                        response.Success = false;
                        response.Message = $"Update document có id {document.Id} không thành công";
                        return response;
                    }
                }
                updateRequestDTO.Documents = null;
                var updated = _mapper.Map(updateRequestDTO, getRequestById);
                _unitOfWork.RequestRepository.Update(updated);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var result = _mapper.Map<UpdateRequestDTO>(updated);
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
