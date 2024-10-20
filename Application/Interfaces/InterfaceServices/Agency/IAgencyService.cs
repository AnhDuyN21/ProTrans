using Application.Commons;
using Application.ViewModels.AgencyDTOs;

namespace Application.Interfaces.InterfaceServices.Agency
{
    public interface IAgencyService
    {
        Task<ServiceResponse<IEnumerable<AgencyDTO>>> GetAgencyAsync();
        Task<ServiceResponse<AgencyDTO>> GetAgencyByIdAsync(Guid id);
        Task<ServiceResponse<CUAgencyDTO>> CreateAgencyAsync(CUAgencyDTO cUAgencyDTO);
        Task<ServiceResponse<CUAgencyDTO>> UpdateAgencyAsync(Guid id, CUAgencyDTO cUAgencyDTO);
        Task<ServiceResponse<bool>> DeleteAgencyAsync(Guid id);
    }
}
