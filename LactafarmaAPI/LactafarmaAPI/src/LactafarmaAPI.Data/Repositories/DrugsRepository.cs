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
    public class DrugsRepository : DataRepositoryBase<Drug, LactafarmaContext, User>, IDrugRepository
    {
        private ILogger<DrugsRepository> _logger;

        #region Constructors

        public DrugsRepository(LactafarmaContext context, ILogger<DrugsRepository> logger) : base(context)
        {
            User = new User
            {
                LanguageId = Guid.Parse("7C0AFE0E-0B25-4AEA-8AAE-51CBDDE1B134")
            };

            _logger = logger;
        }

        #endregion

        #region Public Methods

        public IEnumerable<DrugMultilingual> GetDrugsByGroup(int groupId)
        {
            try
            {
                return EntityContext.DrugsMultilingual.Where(l => l.LanguageId == User.LanguageId).Include(d => d.Drug)
                    .ThenInclude(g => g.Group).Where(gm => gm.Drug.GroupId == groupId).AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetDrugsByGroup with message: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<DrugBrand> GetDrugsByBrand(int brandId)
        {
            try
            {
                return EntityContext.DrugBrands.Where(db => db.BrandId == brandId).Include(d => d.Drug)
                    .ThenInclude(dm => dm.DrugsMultilingual)
                    .Where(dm => dm.Drug.DrugsMultilingual.FirstOrDefault().LanguageId == User.LanguageId).AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetDrugsByBrand with message: {ex.Message}");
                return null;
            }
        }

        public DrugMultilingual GetDrug(int drugId)
        {
            try
            {
                return EntityContext.DrugsMultilingual.Where(e => e.DrugId == drugId && e.LanguageId == User.LanguageId)
                    .Include(e => e.Drug).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetDrug with message: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Overridden Members

        protected override Expression<Func<Drug, bool>> IdentifierPredicate(int id)
        {
            return e => e.Id == id;
        }

        #endregion
    }
}