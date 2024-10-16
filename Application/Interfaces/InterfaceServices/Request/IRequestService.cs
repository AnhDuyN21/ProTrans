using Application.Commons;
using Application.ViewModels.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.Request
{
    public interface IRequestService
    {
        Task<ServiceResponse<IEnumerable<RequestDTO>>> GetRequestAsync();
        Task<ServiceResponse<RequestDTO>> GetRequestByIdAsync(Guid id);
        Task<ServiceResponse<CreateRequestDTO>> CreateRequestAsync(CreateRequestDTO createRequestDTO);
        Task<ServiceResponse<UpdateRequestDTO>> UpdateRequestAsync(Guid id, UpdateRequestDTO updateRequestDTO);
        Task<ServiceResponse<bool>> DeleteRequestAsync(Guid id);
    }
}
