using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Interfaces.InterfaceRepositories
{
	public interface IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null);
		Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null);
		Task<TEntity?> GetByIdAsync(Guid id);
		Task AddAsync(TEntity entity);
		void Update(TEntity entity);
		void UpdateRange(List<TEntity> entities);
		void SoftRemove(TEntity entity);
		Task AddRangeAsync(List<TEntity> entities);
		void SoftRemoveRange(List<TEntity> entities);
		void DeleteRange(List<TEntity> entities);
		Task DeleteRangeAsync(IEnumerable<TEntity> entities);

	}
}
