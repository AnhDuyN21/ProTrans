using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Documents;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
		public async Task<List<Document>> GetByOrderIdAsync(Guid id)
		{
			var result = await _dbSet.Where(x => x.OrderId.Equals(id)).ToListAsync();
			return result;
		}
	}
}
