using Application.Commons;
using Application.ViewModels.RequestDTOs;

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
