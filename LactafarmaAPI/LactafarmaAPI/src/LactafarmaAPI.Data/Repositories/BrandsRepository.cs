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


        protected override Expression<Func<Brand, bool>> IdentifierPredicate(int id)
        {
            return (e => e.Id == id);
        }
    }
}
