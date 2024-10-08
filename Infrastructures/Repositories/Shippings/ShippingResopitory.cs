using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Shippings;
using Domain.Entities;

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
    }
}