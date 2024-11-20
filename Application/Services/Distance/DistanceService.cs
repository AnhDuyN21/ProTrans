using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Distance;
using Application.ViewModels.AgencyDTOs;
using Application.ViewModels.DistanceDTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Distance
{
    public class DistanceService : IDistanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DistanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<IEnumerable<DistanceDTO>>> GetAsync()
        {
            var response = new ServiceResponse<IEnumerable<DistanceDTO>>();

            try
            {
                var list = await _unitOfWork.DistanceRepository.GetAllAsync();
                var result = _mapper.Map<List<DistanceDTO>>(list);

                if (result.Count != 0)
                {
                    response.Success = true;
                    response.Message = "List retrieved successfully";
                    response.Data = result;
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
        public async Task<ServiceResponse<DistanceDTO>> GetByIdAsync(Guid distanceId)
        {
            var response = new ServiceResponse<DistanceDTO>();

            var getById = await _unitOfWork.DistanceRepository.GetByIdAsync(distanceId);
            if (getById == null)
            {
                response.Success = false;
                response.Message = "Id is not existed";
            }
            else
            {
                response.Success = true;
                response.Message = "Agency found";
                response.Data = _mapper.Map<DistanceDTO>(getById);
            }

            return response;
        }
        public async Task<ServiceResponse<CreateUpdateDistanceDTO>> CreateAsync(CreateUpdateDistanceDTO dto)
        {
            var response = new ServiceResponse<CreateUpdateDistanceDTO>();
            try
            {
                var distance = _mapper.Map<Domain.Entities.Distance>(dto);
                await _unitOfWork.DistanceRepository.AddAsync(distance);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var result = _mapper.Map<CreateUpdateDistanceDTO>(distance);
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
        public async Task<ServiceResponse<CreateUpdateDistanceDTO>> UpdateAsync(Guid distanceId, CreateUpdateDistanceDTO dto)
        {
            var response = new ServiceResponse<CreateUpdateDistanceDTO>();
            try
            {
                var getById = await _unitOfWork.DistanceRepository.GetAsync(x => x.Id == distanceId);
                if (getById == null)
                {
                    response.Success = false;
                    response.Message = "Id not exist!";
                    return response;
                }
                var updated = _mapper.Map(dto, getById);
                _unitOfWork.DistanceRepository.Update(updated);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var result = _mapper.Map<CreateUpdateDistanceDTO>(updated);
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
        public async Task<ServiceResponse<bool>> DeleteAsync(Guid distanceId)
        {
            var response = new ServiceResponse<bool>();

            var getById = await _unitOfWork.DistanceRepository.GetAsync(x => x.Id == distanceId);
            if (getById == null)
            {
                response.Success = false;
                response.Message = "Id is not exist";
                return response;
            }

            try
            {
                _unitOfWork.DistanceRepository.SoftRemove(getById);

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
