using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.DocumentHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.DocumentHistory
{
    public class DocumentHistoryRepository : GenericRepository<Domain.Entities.DocumentHistory>, IDocumentHistoryRepository
    {
        private readonly AppDbContext _dbContext;
        public DocumentHistoryRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    }
}
