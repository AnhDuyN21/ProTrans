using Application.Commons;
using Application.ViewModels.AssignmentNotarizationDTOs;
using Application.ViewModels.NotificationDTOs;

namespace Application.Interfaces.InterfaceServices.NotarizationDetail
{
    public interface INotarizationDetailService
    {
        public Task<ServiceResponse<IEnumerable<NotarizationDetailDTO>>> UpdateAllNotarizationDetailsByTaskId(Guid Id);
        public Task<ServiceResponse<IEnumerable<NotarizationDetailDTO>>> GetAllNotarizationDetails(Guid Id);
    }
}
