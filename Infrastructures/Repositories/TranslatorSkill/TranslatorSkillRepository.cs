using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.TranslatorSkill;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructures.Repositories.TranslatorSkill
{
    public class TranslatorSkillRepository : GenericRepository<Domain.Entities.TranslatorSkill>, ITranslatorSkillRepository
    {
        private readonly AppDbContext _dbContext;
        public TranslatorSkillRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task<List<Domain.Entities.TranslatorSkill>> GetAllTranslatorSkillAsync(Expression<Func<Domain.Entities.TranslatorSkill, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Domain.Entities.TranslatorSkill> query = _dbSet

            .Select(q => new Domain.Entities.TranslatorSkill
            {
                Id = q.Id, // Assuming you want Id in the final result
                TranslatorId = q.TranslatorId,
                LanguageId = q.LanguageId, // Use constructor with Name property
                CertificateUrl = q.CertificateUrl,
                IsDeleted = q.IsDeleted,
            })
            .AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);

                }
            }
            return await query.ToListAsync();
        }

		public async Task<List<Guid>> GetTranslatorIdsByLanguageIdAsync(Guid id)
        {
            var result = await _dbSet.Where(x => x.LanguageId  == id).Select(x => x.TranslatorId).ToListAsync();
            return result;
        }

		public async Task<Domain.Entities.TranslatorSkill> GetTranslatorSkillByIdAsync(Expression<Func<Domain.Entities.TranslatorSkill, bool>>? filter = null, string? includeProperties = null)
        {
            var query = _dbSet
           .Select(q => new Domain.Entities.TranslatorSkill
           {
               Id = q.Id, // Assuming you want Id in the final result
               TranslatorId = q.TranslatorId,
               LanguageId = q.LanguageId, // Use constructor with Name property
               CertificateUrl = q.CertificateUrl
           })
           .AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);

                }
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
