
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
                .Include(e => e.Drug)
                .Include(e => e.AliasMultilingual.Where(l => l.LanguageId == User.LanguageId))
                .AsEnumerable();
        }

        protected override Expression<Func<Alias, bool>> IdentifierPredicate(int id)
        {
            return (e => e.Id == id);
        }
    }
}
