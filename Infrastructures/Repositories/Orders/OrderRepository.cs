using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Orders;
using Domain.Entities;
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

		public async Task<Order> GetByPhoneNumberAsync(string num)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.PhoneNumber.Equals(num));
            return result;
        }
    }
}