﻿using Application.Interfaces;
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
                Language = new Domain.Entities.Language { Name = q.Language.Name }, // Use constructor with Name property
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

        public Task<Domain.Entities.TranslatorSkill> GetTranslatorSkillByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}