using Application.Commons;
using Application.ViewModels.RequestDTOs;

namespace Application.Interfaces.InterfaceServices.Request
{
    public interface IRequestService
    {
        Task<ServiceResponse<IEnumerable<RequestDTO>>> GetRequestAsync();
        Task<ServiceResponse<IEnumerable<RequestDTO>>> GetRequestByCustomerAsync(Guid customerId);
        Task<ServiceResponse<IEnumerable<RequestDTO>>> GetRequestWithStatusAsync(string status);
        Task<ServiceResponse<RequestDTO>> GetRequestByIdAsync(Guid id);
        Task<ServiceResponse<CreateRequestDTO>> CreateRequestAsync(CreateRequestDTO createRequestDTO);
        Task<ServiceResponse<UpdateRequestDTO>> UpdateRequestAsync(Guid id, UpdateRequestDTO updateRequestDTO);
        Task<ServiceResponse<RequestDTO>> UpdateRequestByCustomerAsync(Guid id, CustomerUpdateRequestDTO customerUpdateRequestDTO);
        Task<ServiceResponse<bool>> DeleteRequestAsync(Guid id);
    }
}
