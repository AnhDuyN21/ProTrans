using Application.Commons;
using Application.ViewModels.DistanceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.Distance
{
    public interface IDistanceService
    {
        Task<ServiceResponse<IEnumerable<DistanceDTO>>> GetAsync();
        Task<ServiceResponse<DistanceDTO>> GetByIdAsync(Guid distanceId);
        Task<ServiceResponse<CreateUpdateDistanceDTO>> CreateAsync(CreateUpdateDistanceDTO dto);
        Task<ServiceResponse<CreateUpdateDistanceDTO>> UpdateAsync(Guid distanceId, CreateUpdateDistanceDTO dto);
        Task<ServiceResponse<bool>> DeleteAsync(Guid distanceId);
    }
}
