using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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