using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.DocumentType;

namespace Infrastructures.Repositories.DocumentType
{
    public class DocumentTypeRepository : GenericRepository<Domain.Entities.DocumentType>, IDocumentTypeRepository
    {
        private readonly AppDbContext _dbContext;
        public DocumentTypeRepository(
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

