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
        public async Task<decimal> CaculateDocumentNotarizationPrice(bool notarizationRequest, Guid? notarizationId, int numberOfNotarizedCopies)
        {
            decimal notarizationPrice = 0;

            if (notarizationRequest == false) return notarizationPrice;

            var notarization = await _dbContext.Notarization.FirstOrDefaultAsync(x => x.Id == notarizationId);
            if (notarization == null) return notarizationPrice;

            notarizationPrice = notarization.Price * numberOfNotarizedCopies;
            
            return notarizationPrice;
        }
        public async Task<decimal> CaculateDocumentTranslationPrice(Guid? firstLanguageId, Guid? secondLanguageId, Guid? documentTypeId, int pageNumber, int numberOfCopies)
        {
            decimal translationPrice = 0;
            var quotePrice = await _dbContext.QuotePrice.FirstOrDefaultAsync(x => x.FirstLanguageId == firstLanguageId &&
                                                  x.SecondLanguageId == secondLanguageId);

            var documentType = await _dbContext.DocumentType.FirstOrDefaultAsync(x => x.Id == documentTypeId);

            translationPrice += (decimal)quotePrice.PricePerPage * pageNumber * documentType.PriceFactor;

            translationPrice += (numberOfCopies - 1) * (pageNumber * 500 + 10000);
            return translationPrice;
        }
    }
}
