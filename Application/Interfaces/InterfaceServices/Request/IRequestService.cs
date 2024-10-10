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
        Task<ServiceResponse<CreateRequestDTO>> UpdateRequestAsync(Guid id, CreateRequestDTO createRequestDTO);
        Task<ServiceResponse<bool>> DeleteRequestAsync(Guid id);
    }
}
