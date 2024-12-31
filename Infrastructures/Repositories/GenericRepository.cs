using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructures.Repositories
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		private readonly AppDbContext _context;
		protected DbSet<TEntity> _dbSet;
		private readonly ICurrentTime _timeService;
		private readonly IClaimsService _claimsService;

		public GenericRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService)
		{
			_context = context;
			_dbSet = context.Set<TEntity>();
			_timeService = timeService;
			_claimsService = claimsService;
		}
		public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
		{
			IQueryable<TEntity> query = _dbSet;
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

		public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
		{
			IQueryable<TEntity> query = _dbSet;
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
			return await query.FirstOrDefaultAsync();
		}

		public async Task<TEntity?> GetByIdAsync(Guid id)
		{
			var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
			return result;
		}

		public async Task AddAsync(TEntity entity)
		{
			entity.CreatedDate = _timeService.GetCurrentTime();
			entity.CreatedBy = _claimsService.GetCurrentUserId;
			await _dbSet.AddAsync(entity);
		}

		public void SoftRemove(TEntity entity)
		{
			entity.IsDeleted = true;
			entity.DeletedBy = _claimsService.GetCurrentUserId;
			_dbSet.Update(entity);
		}

		public void Update(TEntity entity)
		{
			entity.ModifiedDate = _timeService.GetCurrentTime();
			entity.ModifiedBy = _claimsService.GetCurrentUserId;
			_dbSet.Update(entity);
		}

		public async Task AddRangeAsync(List<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				entity.CreatedDate = _timeService.GetCurrentTime();
				entity.CreatedBy = _claimsService.GetCurrentUserId;
			}
			await _dbSet.AddRangeAsync(entities);
		}

		public void SoftRemoveRange(List<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				entity.IsDeleted = true;
				entity.DeletedDate = _timeService.GetCurrentTime();
				entity.DeletedBy = _claimsService.GetCurrentUserId;
			}
			_dbSet.UpdateRange(entities);
		}

		public void UpdateRange(List<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				entity.CreatedDate = _timeService.GetCurrentTime();
				entity.CreatedBy = _claimsService.GetCurrentUserId;
			}
			_dbSet.UpdateRange(entities);
		}
		public async void DeleteRange(List<TEntity> entities)
		{
			_dbSet.RemoveRange(entities);
		}
		public Task DeleteRangeAsync(IEnumerable<TEntity> entities)
		{
			_dbSet.RemoveRange(entities);
			return Task.CompletedTask;
		}
	}
}
