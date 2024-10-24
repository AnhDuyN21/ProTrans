﻿using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.AssignmentTranslation;
using Application.ViewModels.AssignmentTranslationDTOs;
using AutoMapper;
using Domain.Enums;
using System.Data.Common;

namespace Application.Services.AssignmentTranslation
{
    public class AssignmentTranslationService : IAssignmentTranslationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AssignmentTranslationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<AssignmentTranslationDTO>> CreateAssignmentTranslationAsync(CUAssignmentTranslationDTO cuAssignmentTranslationDTO)
        {
            var response = new ServiceResponse<AssignmentTranslationDTO>();
            try
            {
                var assignmentTranslation = _mapper.Map<Domain.Entities.AssignmentTranslation>(cuAssignmentTranslationDTO);
                assignmentTranslation.Status = AssignmentNotarizationStatus.Waiting.ToString();

                await _unitOfWork.AssignmentTranslationRepository.AddAsync(assignmentTranslation);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var assignmentTranslationDTO = _mapper.Map<AssignmentTranslationDTO>(assignmentTranslation);
                    response.Data = assignmentTranslationDTO;
                    response.Success = true;
                    response.Message = "Assignment Translation created successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the Assignment Translation.";
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

        public async Task<ServiceResponse<bool>> DeleteAssignmentTranslationAsync(Guid id)
        {
            var response = new ServiceResponse<bool>();

            var assignmentTranslationGetById = await _unitOfWork.AssignmentTranslationRepository.GetByIdAsync(id);
            if (assignmentTranslationGetById == null)
            {
                response.Success = false;
                response.Message = "Assignment Translation is not existed";
                return response;
            }

            try
            {
                _unitOfWork.AssignmentTranslationRepository.SoftRemove(assignmentTranslationGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Assignment Translation deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error deleting the Assignment Translation.";
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

        public async Task<ServiceResponse<IEnumerable<AssignmentTranslationDTO>>> GetAllAssignmentTranslationsAsync()
        {
            var response = new ServiceResponse<IEnumerable<AssignmentTranslationDTO>>();

            try
            {
                var AssignmentTranslationList = await _unitOfWork.AssignmentTranslationRepository.GetAllAsync(x => x.IsDeleted == false);
                var AssignmentTranslationDTOs = _mapper.Map<List<AssignmentTranslationDTO>>(AssignmentTranslationList);

                if (AssignmentTranslationDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Assignment Translation list retrieved successfully";
                    response.Data = AssignmentTranslationDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have Assignment Translation in list";
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

        public async Task<ServiceResponse<IEnumerable<AssignmentTranslationDTO>>> GetAllAssignmentTranslationByTranslatorIdAsync(Guid Id)
        {
            var response = new ServiceResponse<IEnumerable<AssignmentTranslationDTO>>();

            try
            {
                var AssignmentTranslationList = await _unitOfWork.AssignmentTranslationRepository.GetAllAsync(x => x.TranslatorId.Equals(Id) && x.IsDeleted == false);
                var AssignmentTranslationDTOs = _mapper.Map<List<AssignmentTranslationDTO>>(AssignmentTranslationList);

                if (AssignmentTranslationDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Assignment Translation list retrieved successfully";
                    response.Data = AssignmentTranslationDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Not have Assignment Translation in list";
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

        public async Task<ServiceResponse<AssignmentTranslationDTO>> UpdateAssignmentTranslationAsync(Guid id, CUAssignmentTranslationDTO cuAssignmentTranslationDTO)
        {
            var response = new ServiceResponse<AssignmentTranslationDTO>();

            try
            {
                var assignmentTranslationGetById = await _unitOfWork.AssignmentTranslationRepository.GetByIdAsync(id);

                if (assignmentTranslationGetById == null)
                {
                    response.Success = false;
                    response.Message = "Assignment Translation not found.";
                    return response;
                }
                if ((bool)assignmentTranslationGetById.IsDeleted)
                {
                    response.Success = false;
                    response.Message = "Assignment Translation is deleted in system";
                    return response;
                }
                // Map assignmentTranslationDT0 => existingUser
                var objectToUpdate = _mapper.Map(cuAssignmentTranslationDTO, assignmentTranslationGetById);


                _unitOfWork.AssignmentTranslationRepository.Update(assignmentTranslationGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Data = _mapper.Map<AssignmentTranslationDTO>(objectToUpdate);
                    response.Success = true;
                    response.Message = "AssignmentTranslation updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error updating the Aassignment Translation.";
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
        public async Task<ServiceResponse<AssignmentTranslationDTO>> UpdateStatusAssignmentTranslationAsync(Guid id, AssignmentTranslationStatus status)
        {
            var response = new ServiceResponse<AssignmentTranslationDTO>();

            try
            {
                var assignmentTranslationGetById = await _unitOfWork.AssignmentTranslationRepository.GetByIdAsync(id);

                if (assignmentTranslationGetById == null)
                {
                    response.Success = false;
                    response.Message = "Assignment Translation not found.";
                    return response;
                }
                if ((bool)assignmentTranslationGetById.IsDeleted)
                {
                    response.Success = false;
                    response.Message = "Assignment Translation is deleted in system";
                    return response;
                }
                // Map assignmentTranslationDT0 => existingUser

                assignmentTranslationGetById.Status = status.ToString();

                _unitOfWork.AssignmentTranslationRepository.Update(assignmentTranslationGetById);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Data = _mapper.Map<AssignmentTranslationDTO>(assignmentTranslationGetById);
                    response.Success = true;
                    response.Message = "AssignmentTranslation updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error updating the Aassignment Translation.";
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

