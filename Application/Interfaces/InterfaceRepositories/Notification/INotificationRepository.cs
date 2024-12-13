using Org.BouncyCastle.Asn1;
using System.Linq.Expressions;

namespace Application.Interfaces.InterfaceRepositories.Notification
{
    public interface INotificationRepository
    {
        public Task SendNotificationAsync(Domain.Entities.Notification notification, Guid id);
        public Task SendANotificationAsync(Domain.Entities.Notification notification, Guid id);
        public Task<List<Domain.Entities.Notification>> GetAllNotificationAsync(Expression<Func<Domain.Entities.Notification, bool>>? filter = null,
              Func<IQueryable<Domain.Entities.Notification>, IOrderedQueryable<Domain.Entities.Notification>>? orderBy = null, 
              string? includeProperties = null);
        public void UpdateNotificationAsync(Domain.Entities.Notification notification);
        public  Task<Domain.Entities.Notification> GetByIdAsync(int id);

    }
}
