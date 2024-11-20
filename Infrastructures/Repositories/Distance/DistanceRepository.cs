using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Distance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.Distance
{
    public class DistanceRepository : GenericRepository<Domain.Entities.Distance>, IDistanceRepository
    {
        private readonly AppDbContext _dbContext;
        public DistanceRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    }
}
