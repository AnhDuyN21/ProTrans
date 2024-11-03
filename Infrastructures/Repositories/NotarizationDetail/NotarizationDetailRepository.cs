using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.NotarizationDetail;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructures.Repositories.NotarizationDetail
{
    public class NotarizationDetailRepository : GenericRepository<Domain.Entities.NotarizationDetail>, INotarizationDetailRepository
    {
        private readonly AppDbContext _dbContext;
        public NotarizationDetailRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task AddManyNotarizationDetails(Guid id,Guid[] Docid)
        {
            foreach (var notarizationDetail in Docid)
            {
                var NotarizationDetail = new Domain.Entities.NotarizationDetail
                {
                    AssignmentNotarizationId = id,
                    DocumentId = notarizationDetail
                };
                await _dbSet.AddAsync( NotarizationDetail );
            }
            return;
        }
    }
}