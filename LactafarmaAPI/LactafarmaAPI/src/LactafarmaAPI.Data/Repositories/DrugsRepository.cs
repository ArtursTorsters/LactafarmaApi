using LactafarmaAPI.Core;
using LactafarmaAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using LactafarmaAPI.Data.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LactafarmaAPI.Data.Repositories
{
    public class DrugsRepository : DataRepositoryBase<Drug, LactafarmaContext, User>, IDrugRepository
    {
        public DrugsRepository(LactafarmaContext context, User user): base(context, user)
        {           
        }        

        public IEnumerable<Drug> GetDrugsByGroup(int groupId)
        {
            return EntityContext.Drugs
             .Where(e => e.GroupId == groupId)
             .Include(e => e.Group)
             .Include(e => e.DrugsMultilingual.Where(l => l.LanguageId == User.LanguageId))
             .AsEnumerable();
        }

        public IEnumerable<Drug> GetDrugsByBrand(int brandId)
        {
            return EntityContext.Drugs
             .Include(e => e.DrugBrands.Where(b => b.BrandId == brandId))                  
             .Include(e => e.DrugsMultilingual.Where(l => l.LanguageId == User.LanguageId))
             .AsEnumerable();
        }

        public Drug GetDrug(int drugId)
        {
            return EntityContext.Drugs.Where(e => e.Id == drugId)
                .Include(e => e.DrugsMultilingual.Where(l => l.LanguageId == User.LanguageId)).FirstOrDefault();
        }

        protected override Expression<Func<Drug, bool>> IdentifierPredicate(int id)
        {
            return (e => e.Id == id);
        }
    }
}
