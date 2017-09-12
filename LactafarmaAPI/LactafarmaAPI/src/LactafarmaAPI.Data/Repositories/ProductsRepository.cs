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
    public class ProductsRepository : DataRepositoryBase<Product, LactafarmaContext>, IProductRepository
    {
        private readonly ILogger<ProductsRepository> _logger;

        #region Constructors

        public ProductsRepository(LactafarmaContext context, ILogger<ProductsRepository> logger, IHttpContextAccessor httpContext) : base(context, httpContext)
        {
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public IEnumerable<ProductMultilingual> GetAllProducts()
        {
            try
            {
                return EntityContext.ProductsMultilingual.Where(l => l.LanguageId == LanguageId).Include(d => d.Product)
                    .AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllProducts with message: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<ProductGroup> GetProductsByGroup(int groupId)
        {
            try
            {
                return EntityContext.ProductGroups.Where(db => db.GroupId == groupId).Include(d => d.Product)
                    .ThenInclude(dm => dm.ProductsMultilingual)
                    .Where(dm => dm.Product.ProductsMultilingual.FirstOrDefault().LanguageId == LanguageId).AsEnumerable();                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetProductsByGroup with message: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<ProductBrand> GetProductsByBrand(int brandId)
        {
            try
            {
                return EntityContext.ProductBrands.Where(db => db.BrandId == brandId).Include(d => d.Product)
                    .ThenInclude(dm => dm.ProductsMultilingual)
                    .Where(dm => dm.Product.ProductsMultilingual.FirstOrDefault().LanguageId == LanguageId).AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetProductsByBrand with message: {ex.Message}");
                return null;
            }
        }

        public ProductMultilingual GetProduct(int productId)
        {
            try
            {
                return EntityContext.ProductsMultilingual.Where(e => e.ProductId == productId && e.LanguageId == LanguageId)
                    .Include(e => e.Product).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetProduct with message: {ex.Message}");
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