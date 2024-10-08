using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Feedbacks;
using Domain.Entities;

namespace Infrastructures.Repositories.Feedbacks
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        private readonly AppDbContext _dbContext;
        public FeedbackRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    }
}