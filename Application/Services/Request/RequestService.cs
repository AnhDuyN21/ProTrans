using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Request;
using Application.ViewModels.DocumentTypeDTOs;
using Application.ViewModels.RequestDTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var request = _mapper.Map<Domain.Entities.Request>(createRequestDTO);
                var customerId = _unitOfWork.RequestRepository.GetCurrentCustomerId();
                request.CustomerId = customerId;
                await _unitOfWork.RequestRepository.AddAsync(request);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var result = _mapper.Map<CreateRequestDTO>(request);
                    response.Data = result;
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
        public async Task<ServiceResponse<CreateRequestDTO>> UpdateRequestAsync(Guid id, CreateRequestDTO createRequestDTO)
        {
            var response = new ServiceResponse<CreateRequestDTO>();
            try
            {
                var getRequestById = await _unitOfWork.RequestRepository.GetByIdAsync(id);
                if (getRequestById == null)
                {
                    response.Success = false;
                    response.Message = "Id not exist!";
                    return response;
                }
                var updated = _mapper.Map(createRequestDTO, getRequestById);
                _unitOfWork.RequestRepository.Update(updated);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var result = _mapper.Map<CreateRequestDTO>(updated);
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
