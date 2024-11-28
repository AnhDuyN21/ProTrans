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
        public async Task<decimal> CaculateDocumentPrice(Guid? firstLanguageId, Guid? secondLanguageId, Guid? documentTypeId, int pageNumber, int numberOfCopies, bool notarizationRequest, Guid? notarizationId, int numberOfNotarizedCopies)
        {
            decimal price = 0;
            var quotePrice = await _dbContext.QuotePrice.FirstOrDefaultAsync(x => x.FirstLanguageId == firstLanguageId &&
                                                              x.SecondLanguageId == secondLanguageId);
            if(quotePrice == null) return price;

            var documentType = await _dbContext.DocumentType.FirstOrDefaultAsync(x => x.Id == documentTypeId);
            if (documentType == null) return price;

            price += (decimal)quotePrice.PricePerPage * pageNumber * documentType.PriceFactor;

            price += (numberOfCopies - 1) * (pageNumber * 500 + 10000);
            if (notarizationRequest)
            {
                var notarization = await _dbContext.Notarization.FirstOrDefaultAsync(x => x.Id == notarizationId) ;
                if (notarization != null)
                {
                    price += notarization.Price * numberOfNotarizedCopies;
                }
            }
            return price;
        }

    }
}
