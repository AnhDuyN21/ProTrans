using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.AssignmentTranslation;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.AssignmentTranslation
{
    public class AssignmentTranslationRepository : GenericRepository<Domain.Entities.AssignmentTranslation>, IAssignmentTranslationRepository
    {
        private readonly AppDbContext _dbContext;
        public AssignmentTranslationRepository(
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
