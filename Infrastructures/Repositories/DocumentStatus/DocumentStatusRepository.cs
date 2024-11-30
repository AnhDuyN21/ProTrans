using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.DocumentStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.DocumentStatus
{
    public class DocumentStatusRepository : GenericRepository<Domain.Entities.DocumentStatus>, IDocumentStatusRepository
    {
        private readonly AppDbContext _dbContext;
        public DocumentStatusRepository(
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
