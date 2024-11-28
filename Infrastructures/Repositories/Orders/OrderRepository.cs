using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Orders;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories.Orders
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IClaimsService _claimsService;
        public OrderRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
            _claimsService = claimsService;
        }

        public Guid GetCurrentStaffId()
        {
            var id = _claimsService.GetCurrentUserId;
            return id;
        }

        public async Task<List<Order>> GetByPhoneNumberAsync(string num)
        {
            var result = await _dbSet.Where(x => x.PhoneNumber.Equals(num)).ToListAsync();
            return result;
        }
        public async Task<Order> GetByDocumentId(Guid? documentId)
        {
            var order = await _dbContext.Order
                                        .Include(o => o.Documents)
                                        .FirstOrDefaultAsync(o => o.Documents.Any(d => d.Id == documentId));

            return order;
        }
        public async Task<bool> UpdateOrderStatusByDocumentId(Guid documentId)
        {
            var order = await _dbContext.Order
                                        .Include(o => o.Documents)
                                        .FirstOrDefaultAsync(o => o.Documents.Any(d => d.Id == documentId));
            if(order == null) return false;
            if (order.Status != OrderStatus.Implementing.ToString()) return false;
            bool allDocumentsTranslated = order.Documents.All(document => document.TranslationStatus == DocumentTranslationStatus.Translated.ToString());
            bool anyDocumentNotarized = order.Documents.Any(d => d.NotarizationRequest == true);
            bool allNotarizedDocumentsCompleted = order.Documents
                                               .Where(d => d.NotarizationRequest == true)
                                               .All(d => d.NotarizationStatus == DocumentNotarizationStatus.Notarizated.ToString());
            if (allDocumentsTranslated == true && anyDocumentNotarized == false ||
                allDocumentsTranslated == true && allNotarizedDocumentsCompleted == true)
            {
                order.Status = OrderStatus.Completed.ToString();
                Update(order);
                return true;
            }
            return false;

        }
    }
}
