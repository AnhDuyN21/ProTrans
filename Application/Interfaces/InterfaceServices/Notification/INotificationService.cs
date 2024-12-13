using Application.Commons;
using Application.ViewModels.NotificationDTOs;

namespace Application.Interfaces.InterfaceServices.Notification
{
    public interface INotificationService
    {
        public Task<ServiceResponse<NotificationDTO>> SendNotificationAsync(SendNotificationDTO sendNotificationDTO);
        public Task<ServiceResponse<NotificationDTO>> SendANotificationAsync(SendNotificationDTO sendNotificationDTO);
        public Task<ServiceResponse<IEnumerable<NotificationDTO>>> GetAllNotificationAsync(Guid Id);
        public Task<string> GetRoleStringAsync(Guid RoleId);
        public Task<ServiceResponse<NotificationDTO>> Update(int id);
    }
}
