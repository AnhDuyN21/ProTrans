using Application.Interfaces;
using Application.Interfaces.InterfaceRepositories.TranslationSkill;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructures.Repositories.TranslationSkill
{
    public class TranslatorSkillRepository : GenericRepository<Domain.Entities.TranslationSkill>, ITranslatorSkillRepository
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

        public async Task<List<Domain.Entities.TranslationSkill>> GetAllTranslatorSkillAsync(Expression<Func<Domain.Entities.TranslationSkill, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Domain.Entities.TranslationSkill> query = _dbSet

            .Select(q => new Domain.Entities.TranslationSkill
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
            var result = await _dbSet.Where(x => x.LanguageId == id).Select(x => x.TranslatorId).ToListAsync();
            return result;
        }

        public async Task<Domain.Entities.TranslationSkill> GetTranslatorSkillByIdAsync(Expression<Func<Domain.Entities.TranslationSkill, bool>>? filter = null, string? includeProperties = null)
        {
            var query = _dbSet
           .Select(q => new Domain.Entities.TranslationSkill
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
