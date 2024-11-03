using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.IAssignmentShippings;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories.AssignmentShippings
{
	public class AssignmentShippingResopitory : GenericRepository<AssignmentShipping>, IAssignmentShippingRepository
	{
		private readonly AppDbContext _dbContext;
		public AssignmentShippingResopitory(
			AppDbContext context,
			ICurrentTime timeService,
			IClaimsService claimsService
		)
			: base(context, timeService, claimsService)
		{
			_dbContext = context;
		}

		public async Task<List<AssignmentShipping>> GetByShipperIdAsync(Guid id)
		{
			var result = await _dbSet.Where(x => x.ShipperId.Equals(id)).ToListAsync();
			return result;
		}
	}
}