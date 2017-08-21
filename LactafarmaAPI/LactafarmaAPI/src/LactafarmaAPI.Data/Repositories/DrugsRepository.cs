using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using LactafarmaAPI.Core;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace LactafarmaAPI.Data.Repositories
{
    public class DrugsRepository : DataRepositoryBase<Drug, LactafarmaContext>, IDrugRepository
    {
        private readonly ILogger<DrugsRepository> _logger;

        #region Constructors

        public DrugsRepository(LactafarmaContext context, ILogger<DrugsRepository> logger, IHttpContextAccessor httpContext) : base(context, httpContext)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public IEnumerable<DrugMultilingual> GetAllDrugs()
        {
            try
            {
                return EntityContext.DrugsMultilingual.Where(l => l.LanguageId == LanguageId).Include(d => d.Drug)
                    .AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllDrugs with message: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<DrugMultilingual> GetDrugsByGroup(int groupId)
        {
            try
            {
                return EntityContext.DrugsMultilingual.Where(l => l.LanguageId == LanguageId).Include(d => d.Drug)
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
                    .Where(dm => dm.Drug.DrugsMultilingual.FirstOrDefault().LanguageId == LanguageId).AsEnumerable();
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
                return EntityContext.DrugsMultilingual.Where(e => e.DrugId == drugId && e.LanguageId == LanguageId)
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