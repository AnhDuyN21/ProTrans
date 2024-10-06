using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.InterfaceRepositories.PaymentMethods
{
	public interface IPaymenMethodRepository
	{
		Task<List<PaymentMethod>> GetAllAsync(Expression<Func<PaymentMethod, bool>>? filter = null, string? includeProperties = null);
		Task<PaymentMethod?> GetByIdAsync(Guid id);
		Task AddAsync(PaymentMethod entity);
		void Update(PaymentMethod entity);
		void SoftRemove(PaymentMethod entity);
	}
}