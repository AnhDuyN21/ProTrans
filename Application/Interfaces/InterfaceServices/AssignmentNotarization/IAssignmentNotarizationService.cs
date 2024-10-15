using Application.Commons;
using Application.ViewModels.AssignmentNotarizationDTOs;

namespace Application.Interfaces.InterfaceServices.AssignmentNotarization
{
    public interface IAssignmentNotarizationService
    {
        public Task<ServiceResponse<IEnumerable<AssignmentNotarizationDTO>>> GetAllAssignmentNotarizationsAsync();
        public Task<ServiceResponse<IEnumerable<AssignmentNotarizationDTO>>> GetAllAssignmentNotarizationByShipperIdAsync(Guid Id);
        public Task<ServiceResponse<AssignmentNotarizationDTO>> UpdateAssignmentNotarizationAsync(Guid id, CUAssignmentNotarizationDTO cudAssignmentNotarizationDTO);
        public Task<ServiceResponse<AssignmentNotarizationDTO>> CreateAssignmentNotarizationAsync(CUAssignmentNotarizationDTO AssignmentNotarizationDTO);
        public Task<ServiceResponse<bool>> DeleteAssignmentNotarizationAsync(Guid id);
    }
}
