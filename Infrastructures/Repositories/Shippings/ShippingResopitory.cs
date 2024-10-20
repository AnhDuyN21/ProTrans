using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Shippings;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories.Shippings
{
    public class ShippingResopitory : GenericRepository<Shipping>, IShippingRepository
    {
        private readonly AppDbContext _dbContext;
        public ShippingResopitory(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task<List<Shipping>> GetByShipperIdAsync(Guid id)
        {
            var result = await _dbSet.Where(x => x.ShipperId.Equals(id)).ToListAsync();
            return result;
        }
    }
}