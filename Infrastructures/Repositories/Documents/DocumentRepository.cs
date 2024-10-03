using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.Documents;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.Documents
{
	public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
	{
		private readonly AppDbContext _dbContext;
		public DocumentRepository(
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