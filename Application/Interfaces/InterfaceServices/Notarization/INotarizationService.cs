using Application.Commons;
using Application.ViewModels.NotarizationDTOs;

namespace Application.Interfaces.InterfaceServices.Notarization
{
    public interface INotarizationService
    {
        Task<ServiceResponse<IEnumerable<NotarizationDTO>>> GetNotarizationAsync();
        Task<ServiceResponse<NotarizationDTO>> GetNotarizationByIdAsync(Guid id);
        Task<ServiceResponse<CreateNotarizationDTO>> CreateNotarizationAsync(CreateNotarizationDTO createNotarizationDTO);
        Task<ServiceResponse<CreateNotarizationDTO>> UpdateNotarizationAsync(Guid id, CreateNotarizationDTO createNotarizationDTO);
        Task<ServiceResponse<bool>> DeleteNotarizationAsync(Guid id);
    }
}
