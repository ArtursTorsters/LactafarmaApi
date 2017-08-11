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
    public class BrandsRepository : DataRepositoryBase<Brand, LactafarmaContext, User>, IBrandRepository
    {
        public BrandsRepository(LactafarmaContext context) : base(context)
        {
            User = new User()
            {
                LanguageId = Guid.Parse("7C0AFE0E-0B25-4AEA-8AAE-51CBDDE1B134")
            };
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return EntityContext.Brands
                .Include(e => e.BrandsMultilingual.Where(l => l.LanguageId == User.LanguageId))
                .AsEnumerable();
        }

        public IEnumerable<BrandMultilingual> GetBrandsByDrug(int drugId)
        {
            return EntityContext.BrandsMultilingual.Where(l => l.LanguageId == User.LanguageId).Include(a => a.Brand).ThenInclude(d => d.DrugBrands)
                .Where(a => a.Brand.DrugBrands.FirstOrDefault().DrugId == drugId).AsEnumerable();
        }

        public Brand GetBrand(int brandId)
        {
            return EntityContext.Brands.Where(e => e.Id == brandId)
                .Include(e => e.BrandsMultilingual.Where(l => l.LanguageId == User.LanguageId)).FirstOrDefault();
        }


        protected override Expression<Func<Brand, bool>> IdentifierPredicate(int id)
        {
            return (e => e.Id == id);
        }
    }
}
