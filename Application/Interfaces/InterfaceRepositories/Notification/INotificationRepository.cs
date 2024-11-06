using System.Linq.Expressions;

namespace Application.Interfaces.InterfaceRepositories.Notification
{
    public interface INotificationRepository
    {
        public Task SendNotificationAsync(Domain.Entities.Notification notification, Guid id);
        public Task SendANotificationAsync(Domain.Entities.Notification notification, Guid id);
        public Task<List<Domain.Entities.Notification>> GetAllNotificationAsync(Expression<Func<Domain.Entities.Notification, bool>>? filter = null, string? includeProperties = null);
    }
}
