using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Documents;
using Domain.Entities;
using Domain.Enums;
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
        public async Task<bool> UpdateDocumentTranslationStatusByDocumentId(Guid documentId, string status)
        {
            var document = await _dbContext.Document.Where(document => document.Id == documentId).FirstOrDefaultAsync();
            if (document == null) return false;
            if (document.TranslationStatus != DocumentTranslationStatus.Translating.ToString()) return false;
            document.TranslationStatus = status;
            Update(document);
            return true;
        }
    }
}
