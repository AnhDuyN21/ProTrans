using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Transactions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructures.Repositories.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        protected DbSet<Transaction> _dbSet;
        private readonly ICurrentTime _timeService;
        private readonly IClaimsService _claimsService;

        public TransactionRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService)
        {
            _context = context;
            _dbSet = context.Set<Transaction>();
            _timeService = timeService;
            _claimsService = claimsService;
        }
        public async Task<List<Transaction>> GetAllAsync(Expression<Func<Transaction, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Transaction> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' },
                             StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task AddAsync(Transaction entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(Transaction entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(Transaction entity)
        {
            _dbSet.Update(entity);
        }
    }
}