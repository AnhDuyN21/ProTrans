using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Attachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.Attachment
{
    public class AttachmentRepository : GenericRepository<Domain.Entities.Attachment>, IAttachmentRepository
    {
        private readonly AppDbContext _dbContext;
        public AttachmentRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    }
}
