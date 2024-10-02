using Application.Commons;
using Application.ViewModels.AssignmentNotarizationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.AssignmentNotarization
{
    public interface IAssignmentNotarizationService
    {
        public Task<ServiceResponse<IEnumerable<AssignmentNotarizationDTO>>> GetAllAssignmentNotarizationsAsync();
        public Task<ServiceResponse<IEnumerable<AssignmentNotarizationDTO>>> GetAllAssignmentNotarizationByTranslatorIdAsync(Guid Id);
        public Task<ServiceResponse<AssignmentNotarizationDTO>> UpdateAssignmentNotarizationAsync(Guid id, CUAssignmentNotarizationDTO cudAssignmentNotarizationDTO);
        public Task<ServiceResponse<AssignmentNotarizationDTO>> CreateAssignmentNotarizationAsync(CUAssignmentNotarizationDTO AssignmentNotarizationDTO);
        public Task<ServiceResponse<bool>> DeleteAssignmentNotarizationAsync(Guid id);
    }
}
