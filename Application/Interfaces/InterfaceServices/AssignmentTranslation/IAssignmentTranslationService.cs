using Application.Commons;
using Application.ViewModels.AssignmentTranslationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceServices.AssignmentTranslation
{
    public interface IAssignmentTranslationService
    {
          public Task<ServiceResponse<IEnumerable<AssignmentTranslationDTO>>> GetAllAssignmentTranslationsAsync();
        public Task<ServiceResponse<IEnumerable<AssignmentTranslationDTO>>> GetAllAssignmentTranslationByTranslatorIdAsync(Guid Id);
        public Task<ServiceResponse<AssignmentTranslationDTO>> UpdateAssignmentTranslationAsync(Guid id, CUAssignmentTranslationDTO cudAssignmentTranslationDTO);
        public Task<ServiceResponse<AssignmentTranslationDTO>> CreateAssignmentTranslationAsync(CUAssignmentTranslationDTO AssignmentTranslationDTO);
        public Task<ServiceResponse<bool>> DeleteAssignmentTranslationAsync(Guid id);
    }
}
