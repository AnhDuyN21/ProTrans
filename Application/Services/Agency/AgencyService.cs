using Application.Commons;
using Application.Interfaces;
using Application.Interfaces.InterfaceServices.Agency;
using Application.ViewModels.AgencyDTOs;
using AutoMapper;
using System.Data.Common;

namespace Application.Services.Agency
{
    public class AgencyService : IAgencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AgencyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<IEnumerable<AgencyDTO>>> GetAgencyAsync()
        {
            var response = new ServiceResponse<IEnumerable<AgencyDTO>>();

            try
            {
                var agencyList = await _unitOfWork.AgencyRepository.GetAllAsync();
                var result = _mapper.Map<List<AgencyDTO>>(agencyList);

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
        public async Task<ServiceResponse<AgencyDTO>> GetAgencyByIdAsync(Guid id)
        {
            var response = new ServiceResponse<AgencyDTO>();

            var agencyGetById = await _unitOfWork.AgencyRepository.GetByIdAsync(id);
            if (agencyGetById == null)
            {
                response.Success = false;
                response.Message = "Id is not existed";
            }
            else
            {
                response.Success = true;
                response.Message = "Agency found";
                response.Data = _mapper.Map<AgencyDTO>(agencyGetById);
            }

            return response;
        }
        public async Task<ServiceResponse<CUAgencyDTO>> CreateAgencyAsync(CUAgencyDTO cUAgencyDTO)
        {
            var response = new ServiceResponse<CUAgencyDTO>();
            try
            {
                var agency = _mapper.Map<Domain.Entities.Agency>(cUAgencyDTO);
                await _unitOfWork.AgencyRepository.AddAsync(agency);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var result = _mapper.Map<CUAgencyDTO>(agency);
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
        public async Task<ServiceResponse<CUAgencyDTO>> UpdateAgencyAsync(Guid id, CUAgencyDTO cUAgencyDTO)
        {
            var response = new ServiceResponse<CUAgencyDTO>();
            try
            {
                var getAgencyById = await _unitOfWork.AgencyRepository.GetAsync(x => x.Id == id);
                if (getAgencyById == null)
                {
                    response.Success = false;
                    response.Message = "Id not exist!";
                    return response;
                }
                var updated = _mapper.Map(cUAgencyDTO, getAgencyById);
                _unitOfWork.AgencyRepository.Update(updated);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var result = _mapper.Map<CUAgencyDTO>(updated);
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
        public async Task<ServiceResponse<bool>> DeleteAgencyAsync(Guid id)
        {
            var response = new ServiceResponse<bool>();

            var agencyGetById = await _unitOfWork.AgencyRepository.GetAsync(x => x.Id == id);
            if (agencyGetById == null)
            {
                response.Success = false;
                response.Message = "Id is not exist";
                return response;
            }

            try
            {
                _unitOfWork.AgencyRepository.SoftRemove(agencyGetById);

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
