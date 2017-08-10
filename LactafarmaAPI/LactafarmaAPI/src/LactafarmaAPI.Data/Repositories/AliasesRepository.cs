
using LactafarmaAPI.Core;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LactafarmaAPI.Data.Repositories
{
    public class AliasesRepository : DataRepositoryBase<Alias, LactafarmaContext, User>, IAliasRepository
    {
        public AliasesRepository(LactafarmaContext context, User user) : base(context, user)
        {
        }

        public IEnumerable<Alias> GetAliasesByDrug(int drugId)
        {
            return EntityContext.Aliases
                .Where(e => e.DrugId == drugId)
                .Include(e => e.AliasMultilingual.Where(l => l.LanguageId == User.LanguageId))
                .AsEnumerable();
        }

        public Drug GetDrugByAlias(int aliasId)
        {
            var drugId = EntityContext.Aliases.Where(e => e.Id == aliasId)
                .Include(e => e.Drug).Select(e => e.DrugId).FirstOrDefault();

            return EntityContext.Drugs.Where(x => x.Id == drugId)
                .Include(e => e.DrugsMultilingual.Where(l => l.LanguageId == User.LanguageId))
                .FirstOrDefault();
        }

        public Alias GetAlias(int aliasId)
        {
            return EntityContext.Aliases
                .Where(x => x.Id == aliasId)
                .Include(e => e.AliasMultilingual.Where(l => l.LanguageId == User.LanguageId)).FirstOrDefault();
        }

        protected override Expression<Func<Alias, bool>> IdentifierPredicate(int id)
        {
            return (e => e.Id == id);
        }
    }
}
