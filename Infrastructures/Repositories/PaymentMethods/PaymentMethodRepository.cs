using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.PaymentMethods;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.PaymentMethods
{
	public class PaymentMethodRepository : IPaymenMethodRepository
	{
		private readonly AppDbContext _context;
		protected DbSet<PaymentMethod> _dbSet;
		private readonly ICurrentTime _timeService;
		private readonly IClaimsService _claimsService;

		public PaymentMethodRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService)
		{
			_context = context;
			_dbSet = context.Set<PaymentMethod>();
			_timeService = timeService;
			_claimsService = claimsService;
		}
		public async Task<List<PaymentMethod>> GetAllAsync(Expression<Func<PaymentMethod, bool>>? filter = null, string? includeProperties = null)
		{
			IQueryable<PaymentMethod> query = _dbSet;
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

		public async Task<PaymentMethod?> GetByIdAsync(Guid id)
		{
			var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
			return result;
		}

		public async Task AddAsync(PaymentMethod entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public void SoftRemove(PaymentMethod entity)
		{
			_dbSet.Remove(entity);
		}

		public void Update(PaymentMethod entity)
		{
			_dbSet.Update(entity);
		}
	}
}