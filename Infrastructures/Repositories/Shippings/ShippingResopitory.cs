using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Shippings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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