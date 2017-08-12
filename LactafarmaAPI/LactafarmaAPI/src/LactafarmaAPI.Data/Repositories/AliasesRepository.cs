using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LactafarmaAPI.Core;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LactafarmaAPI.Data.Repositories
{
    public class AliasesRepository : DataRepositoryBase<Alias, LactafarmaContext, User>, IAliasRepository
    {
        private readonly ILogger<AliasesRepository> _logger;

        #region Constructors

        public AliasesRepository(LactafarmaContext context, ILogger<AliasesRepository> logger) : base(context)
        {
            User = new User
            {
                LanguageId = Guid.Parse("7C0AFE0E-0B25-4AEA-8AAE-51CBDDE1B134")
            };

            _logger = logger;
        }

        #endregion

        #region Public Methods

        public IEnumerable<AliasMultilingual> GetAllAliases()
        {
            try
            {
                return EntityContext.AliasMultilingual.Where(e => e.LanguageId == User.LanguageId).Include(a => a.Alias)
                    .AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllAliases with message: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<AliasMultilingual> GetAliasesByDrug(int drugId)
        {
            try
            {
                return EntityContext.AliasMultilingual.Where(e => e.LanguageId == User.LanguageId).Include(a => a.Alias)
                    .ThenInclude(d => d.Drug)
                    .Where(a => a.Alias.DrugId == drugId).AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAliasesByDrug with message: {ex.Message}");
                return null;
            }
        }

        public DrugMultilingual GetDrugByAlias(int aliasId)
        {
            try
            {
                var alias = EntityContext.Aliases.Where(d => d.Id == aliasId).Include(d => d.Drug)
                    .Include(dm => dm.Drug.DrugsMultilingual).FirstOrDefault();

                return alias.Drug.DrugsMultilingual.Single(d => d.LanguageId == User.LanguageId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetDrugByAlias with message: {ex.Message}");
                return null;
            }
        }

        public AliasMultilingual GetAlias(int aliasId)
        {
            try
            {
                return EntityContext.AliasMultilingual.Where(am => am.LanguageId == User.LanguageId).Include(a => a.Alias)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAlias with message: {ex.Message}");
                return null;
            }
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