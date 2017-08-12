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
    public class BrandsRepository : DataRepositoryBase<Brand, LactafarmaContext, User>, IBrandRepository
    {
        private readonly ILogger<BrandsRepository> _logger;

        #region Constructors

        public BrandsRepository(LactafarmaContext context, ILogger<BrandsRepository> logger) : base(context)
        {
            User = new User
            {
                LanguageId = Guid.Parse("7C0AFE0E-0B25-4AEA-8AAE-51CBDDE1B134")
            };

            _logger = logger;
        }

        #endregion

        #region Public Methods

        public IEnumerable<BrandMultilingual> GetAllBrands()
        {
            try
            {
                return EntityContext.BrandsMultilingual.Where(l => l.LanguageId == User.LanguageId)
                    .Include(b => b.Brand)
                    .AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllBrands with message: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<BrandMultilingual> GetBrandsByDrug(int drugId)
        {
            try
            {
                return EntityContext.BrandsMultilingual.Where(l => l.LanguageId == User.LanguageId).Include(a => a.Brand)
                    .ThenInclude(d => d.DrugBrands)
                    .Where(a => a.Brand.DrugBrands.FirstOrDefault().DrugId == drugId).AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetBrandsByDrug with message: {ex.Message}");
                return null;
            }
        }

        public BrandMultilingual GetBrand(int brandId)
        {
            try
            {
                return EntityContext.BrandsMultilingual.Where(l => l.LanguageId == User.LanguageId && l.BrandId == brandId)
                    .Include(a => a.Brand).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetBrand with message: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Overridden Members

        protected override Expression<Func<Brand, bool>> IdentifierPredicate(int id)
        {
            return e => e.Id == id;
        }

        #endregion
    }
}