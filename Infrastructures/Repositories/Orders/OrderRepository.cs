using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Orders;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}