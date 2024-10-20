using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Notification;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructures.Repositories.Notification
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;
        protected DbSet<Domain.Entities.Notification> _dbSet;
        private readonly ICurrentTime _timeService;
        private readonly IClaimsService _claimsService;
        public NotificationRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService)
        {
            _context = context;
            _dbSet = context.Set<Domain.Entities.Notification>();
            _timeService = timeService;
            _claimsService = claimsService;
        }



        public async Task<List<Domain.Entities.Notification>> GetAllNotificationAsync(Expression<Func<Domain.Entities.Notification, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Domain.Entities.Notification> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);

                }
            }
            return await query.ToListAsync();

        }

        public async Task SendNotificationAsync(Domain.Entities.Notification notification, Guid roleId)
        {
            notification.NotificationTime = _timeService.GetCurrentTime();
            IQueryable<Domain.Entities.Account> accounts = _context.Account.Where(a => a.RoleId.Equals(roleId));
            await accounts.ToListAsync();
            foreach (var account in accounts)
            {
                var newNotification = new Domain.Entities.Notification
                {
                    NotificationTime = notification.NotificationTime,
                    Title = notification.Title,
                    Author = notification.Author,
                    Message = notification.Message,
                    AccountId = account.Id
                };
                await _dbSet.AddAsync(newNotification);

            }

            return;
        }
    }
}