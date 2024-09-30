using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Notarization;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.NotarizationDTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Notarization
{
    public class NotarizationService : INotarizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NotarizationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<IEnumerable<NotarizationDTO>>> GetNotarizationAsync()
        {
            var response = new ServiceResponse<IEnumerable<NotarizationDTO>>();

            try
            {
                var notarizationList = await _unitOfWork.NotarizationRepository.GetAllAsync();
                var notarizationDTOs = _mapper.Map<List<NotarizationDTO>>(notarizationList);

                if (notarizationDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Notarization list retrieved successfully";
                    response.Data = notarizationDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have notarization in list";
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
        public async Task<ServiceResponse<NotarizationDTO>> GetNotarizationByIdAsync(Guid id)
        {
            var response = new ServiceResponse<NotarizationDTO>();

            var notarizationGetById = await _unitOfWork.NotarizationRepository.GetByIdAsync(id);
            if (notarizationGetById == null)
            {
                response.Success = false;
                response.Message = "Notarization id is not existed";
            }
            else
            {
                response.Success = true;
                response.Message = "Notarization found";
                response.Data = _mapper.Map<NotarizationDTO>(notarizationGetById);
            }
            return response;
        }
        public async Task<ServiceResponse<CreateNotarizationDTO>> CreateNotarizationAsync(CreateNotarizationDTO createNotarizationDTO)
        {
            var response = new ServiceResponse<CreateNotarizationDTO>();
            try
            {
                var notarization = _mapper.Map<Domain.Entities.Notarization>(createNotarizationDTO);
                await _unitOfWork.NotarizationRepository.AddAsync(notarization);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var result = _mapper.Map<CreateNotarizationDTO>(notarization);
                    response.Data = result;
                    response.Success = true;
                    response.Message = "Notarization created successfully.";
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
        public async Task<ServiceResponse<CreateNotarizationDTO>> UpdateNotarizationAsync(Guid id, CreateNotarizationDTO createNotarizationDTO)
        {
            var response = new ServiceResponse<CreateNotarizationDTO>();
            try
            {
                var getNotarizationById = await _unitOfWork.NotarizationRepository.GetByIdAsync(id);
                if (getNotarizationById == null)
                {
                    response.Success = false;
                    response.Message= "Notarization id not exist!";
                    return response;
                }
                var updatedNotarization = _mapper.Map(createNotarizationDTO, getNotarizationById);
                _unitOfWork.NotarizationRepository.Update(updatedNotarization);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var result = _mapper.Map<CreateNotarizationDTO>(updatedNotarization);
                    response.Data = result;
                    response.Success = true;
                    response.Message = "Notarization updated successfully.";
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
        public async Task<ServiceResponse<bool>> DeleteNotarizationAsync(Guid id)
        {
            var response = new ServiceResponse<bool>();

            var notarizationGetById = await _unitOfWork.NotarizationRepository.GetByIdAsync(id);
            if (notarizationGetById == null)
            {
                response.Success = false;
                response.Message = "Notarization id is not exist";
                return response;
            }

            try
            {
                _unitOfWork.NotarizationRepository.SoftRemove(notarizationGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Notarization deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error deleting notarization.";
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
