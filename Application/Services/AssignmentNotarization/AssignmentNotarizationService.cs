﻿using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.AssignmentNotarization;
using Application.ViewModels.AssignmentNotarizationDTOs;
using Application.ViewModels.AssignmentTranslationDTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System.Data.Common;

namespace Application.Services.AssignmentNotarization
{
    public class AssignmentNotarizationService : IAssignmentNotarizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentTime _currentTime;
        public AssignmentNotarizationService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentTime = currentTime;
        }
        public async Task<ServiceResponse<AssignmentNotarizationDTO>> CreateAssignmentNotarizationAsync(CUAssignmentNotarizationDTO cuAssignmentNotarizationDTO)
        {
            var response = new ServiceResponse<AssignmentNotarizationDTO>();
            try
            {
                var assignmentNotarization = _mapper.Map<Domain.Entities.AssignmentNotarization>(cuAssignmentNotarizationDTO);
                assignmentNotarization.Status = AssignmentNotarizationStatus.Notarizating.ToString();
                
                

                await _unitOfWork.AssignmentNotarizationRepository.AddAsync(assignmentNotarization);


                await _unitOfWork.NotarizationDetailRepository.AddManyNotarizationDetails(assignmentNotarization.Id,cuAssignmentNotarizationDTO.DocumentId);

                foreach(Guid id in cuAssignmentNotarizationDTO.DocumentId)
                {
                    var document = await _unitOfWork.DocumentRepository.GetByIdAsync(id);
                    document.NotarizationStatus = DocumentNotarizationStatus.Notarizating.ToString();

                    //Thêm thời gian cập nhật trạng thái document vào bảng document status
                    var documentStatus = new DocumentStatus
                    {
                        DocumentId = document.Id,
                        Status = DocumentNotarizationStatus.Notarizating.ToString(),
                        Type = TypeStatus.Notarization.ToString(),
                        Time = _currentTime.GetCurrentTime()
                    };
                    await _unitOfWork.DocumentStatusRepository.AddAsync(documentStatus);
                    await _unitOfWork.SaveChangeAsync();

                    _unitOfWork.DocumentRepository.Update(document);
                    
                }

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var assignmentNotarizationDTO = _mapper.Map<AssignmentNotarizationDTO>(assignmentNotarization);
                    response.Data = assignmentNotarizationDTO;
                    response.Success = true;
                    response.Message = "Assignment Notarization created successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the Assignment Notarization.";
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

        public async Task<ServiceResponse<bool>> DeleteAssignmentNotarizationAsync(Guid id)
        {
            var response = new ServiceResponse<bool>();

            var assignmentNotarizationGetById = await _unitOfWork.AssignmentNotarizationRepository.GetByIdAsync(id);
            if (assignmentNotarizationGetById == null)
            {
                response.Success = false;
                response.Message = "Assignment Notarization is not existed";
                return response;
            }

            try
            {
                _unitOfWork.AssignmentNotarizationRepository.SoftRemove(assignmentNotarizationGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Assignment Notarization deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error deleting the Assignment Notarization.";
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

        public async Task<ServiceResponse<IEnumerable<AssignmentNotarizationDTO>>> GetAllAssignmentNotarizationsAsync()
        {
            var response = new ServiceResponse<IEnumerable<AssignmentNotarizationDTO>>();

            try
            {
                var AssignmentNotarizationList = await _unitOfWork.AssignmentNotarizationRepository.GetAllAsync(x => x.IsDeleted == false);
                var AssignmentNotarizationDTOs = _mapper.Map<List<AssignmentNotarizationDTO>>(AssignmentNotarizationList);

                if (AssignmentNotarizationDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Assignment Notarization list retrieved successfully";
                    response.Data = AssignmentNotarizationDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have Assignment Notarization in list";
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

        public async Task<ServiceResponse<IEnumerable<AssignmentNotarizationDTO>>> GetAllAssignmentNotarizationByShipperIdAsync(Guid Id)
        {
            var response = new ServiceResponse<IEnumerable<AssignmentNotarizationDTO>>();

            try
            {
                var AssignmentNotarizationList = await _unitOfWork.AssignmentNotarizationRepository.GetAllAsync(x => x.ShipperId.Equals(Id) && x.IsDeleted == false && x.Status != AssignmentNotarizationStatus.Notarized.ToString());
                var AssignmentNotarizationDTOs = _mapper.Map<List<AssignmentNotarizationDTO>>(AssignmentNotarizationList);

                if (AssignmentNotarizationDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Assignment Notarization list retrieved successfully";
                    response.Data = AssignmentNotarizationDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have Assignment Notarization in list";
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

        public async Task<ServiceResponse<AssignmentNotarizationDTO>> UpdateAssignmentNotarizationAsync(Guid id, CUAssignmentNotarizationDTO cuAssignmentNotarizationDTO)
        {
            var response = new ServiceResponse<AssignmentNotarizationDTO>();

            try
            {
                var assignmentNotarizationGetById = await _unitOfWork.AssignmentNotarizationRepository.GetByIdAsync(id);

                if (assignmentNotarizationGetById == null)
                {
                    response.Success = false;
                    response.Message = "Assignment Notarization not found.";
                    return response;
                }
                if ((bool)assignmentNotarizationGetById.IsDeleted)
                {
                    response.Success = false;
                    response.Message = "Assignment Notarization is deleted in system";
                    return response;
                }
                // Map assignmentNotarizationDT0 => existingUser
                var objectToUpdate = _mapper.Map(cuAssignmentNotarizationDTO, assignmentNotarizationGetById);
        
                _unitOfWork.AssignmentNotarizationRepository.Update(assignmentNotarizationGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Data = _mapper.Map<AssignmentNotarizationDTO>(objectToUpdate);
                    response.Success = true;
                    response.Message = "AssignmentNotarization updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error updating the Aassignment Notarization.";
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
        public async Task<ServiceResponse<AssignmentNotarizationDTO>> UpdateStatusAssignmentNotarizationAsync(Guid id, AssignmentNotarizationStatus status)
        {
            var response = new ServiceResponse<AssignmentNotarizationDTO>();

            try
            {
                var assignmentNotarizationGetById = await _unitOfWork.AssignmentNotarizationRepository.GetByIdAsync(id);

                if (assignmentNotarizationGetById == null)
                {
                    response.Success = false;
                    response.Message = "Assignment Notarization not found.";
                    return response;
                }
                if ((bool)assignmentNotarizationGetById.IsDeleted)
                {
                    response.Success = false;
                    response.Message = "Assignment Notarization is deleted in system";
                    return response;
                }

                assignmentNotarizationGetById.Status = status.ToString();


                _unitOfWork.AssignmentNotarizationRepository.Update(assignmentNotarizationGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Data = _mapper.Map<AssignmentNotarizationDTO>(assignmentNotarizationGetById);
                    response.Success = true;
                    response.Message = "AssignmentNotarization updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error updating the Aassignment Notarization.";
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
