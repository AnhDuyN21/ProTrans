﻿using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Feedbacks;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.Feedbacks
{
	public class FeedbackRepository : GenericRepository<FeedBack>, IFeedbackRepository
	{
		private readonly AppDbContext _dbContext;
		public FeedbackRepository(
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