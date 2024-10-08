using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Documents;
using Domain.Entities;

namespace Infrastructures.Repositories.Documents
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        private readonly AppDbContext _dbContext;
        public DocumentRepository(
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