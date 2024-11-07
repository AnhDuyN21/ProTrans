using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Feedbacks;
using Application.Interfaces.InterfaceRepositories.ImageShippings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.ImageShippings
{
	public class ImageShippingRepository : GenericRepository<ImageShipping>, IImageShippingRepository
	{
		private readonly AppDbContext _dbContext;
		public ImageShippingRepository(
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
