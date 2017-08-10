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
        public BrandsRepository(LactafarmaContext context, User user) : base(context, user)
        {
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return EntityContext.Brands                
                .Include(e => e.BrandsMultilingual.Where(l => l.LanguageId == User.LanguageId))
                .AsEnumerable();
        }

        public IEnumerable<Brand> GetBrandsByDrug(int drugId)
        {
            return EntityContext.Brands.Include(e => e.DrugBrands.Where(x => x.DrugId == drugId))
                .Include(e => e.BrandsMultilingual.Where(l => l.LanguageId == User.LanguageId))
                .AsEnumerable();
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
