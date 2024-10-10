using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.Request
{
    public class RequestRepository : GenericRepository<Domain.Entities.Request>, IRequestRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IClaimsService _claimsService;
        public RequestRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
            _claimsService = claimsService;
        }
        public Guid GetCurrentCustomerId()
        {
            var id = _claimsService.GetCurrentUserId;
            return id;
        }
    }
}
