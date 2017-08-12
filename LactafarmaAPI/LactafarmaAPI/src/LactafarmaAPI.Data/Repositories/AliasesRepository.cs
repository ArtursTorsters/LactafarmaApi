using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LactafarmaAPI.Core;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LactafarmaAPI.Data.Repositories
{
    public class AliasesRepository : DataRepositoryBase<Alias, LactafarmaContext, User>, IAliasRepository
    {
        #region Constructors

        public AliasesRepository(LactafarmaContext context) : base(context)
        {
            User = new User
            {
                LanguageId = Guid.Parse("7C0AFE0E-0B25-4AEA-8AAE-51CBDDE1B134")
            };
        }

        #endregion

        #region Public Methods

        public IEnumerable<AliasMultilingual> GetAliasesByDrug(int drugId)
        {
            return EntityContext.AliasMultilingual.Where(e => e.LanguageId == User.LanguageId).Include(a => a.Alias)
                .ThenInclude(d => d.Drug)
                .Where(a => a.Alias.DrugId == drugId).AsEnumerable();
        }

        public DrugMultilingual GetDrugByAlias(int aliasId)
        {
            var alias = EntityContext.Aliases.Where(d => d.Id == aliasId).Include(d => d.Drug)
                .Include(dm => dm.Drug.DrugsMultilingual).FirstOrDefault();

            return alias.Drug.DrugsMultilingual.Single(d => d.LanguageId == User.LanguageId);
        }

        public AliasMultilingual GetAlias(int aliasId)
        {
            return EntityContext.AliasMultilingual.Where(am => am.LanguageId == User.LanguageId).Include(a => a.Alias)
                .FirstOrDefault();
        }

        #endregion

        #region Overridden Members

        protected override Expression<Func<Alias, bool>> IdentifierPredicate(int id)
        {
            return e => e.Id == id;
        }

        #endregion
    }
}