using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Orders;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories.Orders
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _dbContext;
        public OrderRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task<Order> GetByPhoneNumberAsync(string num)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.PhoneNumber.Equals(num));
            return result;
        }
    }
}