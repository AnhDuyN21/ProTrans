using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces.InterfaceRepositories.Transactions
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllAsync(Expression<Func<Transaction, bool>>? filter = null, string? includeProperties = null);
        Task<Transaction?> GetByIdAsync(Guid id);
        Task AddAsync(Transaction entity);
        void Update(Transaction entity);
        void Delete(Transaction entity);
    }
}