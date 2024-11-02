using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.DocumentPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.DocumentPrice
{
    public class DocumentPriceRepository : GenericRepository<Domain.Entities.DocumentPrice>, IDocumentPriceRepository
    {
        private readonly AppDbContext _dbContext;
        public DocumentPriceRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    }
}
