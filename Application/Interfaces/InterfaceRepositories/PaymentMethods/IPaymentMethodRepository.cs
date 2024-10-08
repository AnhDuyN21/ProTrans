using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces.InterfaceRepositories.PaymentMethods
{
    public interface IPaymentMethodRepository
    {
        Task<List<PaymentMethod>> GetAllAsync(Expression<Func<PaymentMethod, bool>>? filter = null, string? includeProperties = null);
        Task<PaymentMethod?> GetByIdAsync(Guid id);
        Task AddAsync(PaymentMethod entity);
        void Update(PaymentMethod entity);
        void Delete(PaymentMethod entity);
    }
}