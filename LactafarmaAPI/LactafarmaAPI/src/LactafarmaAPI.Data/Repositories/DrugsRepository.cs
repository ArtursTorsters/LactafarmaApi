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
    public class DrugsRepository : DataRepositoryBase<Drug, LactafarmaContext, User>, IDrugRepository
    {
        #region Constructors

        public DrugsRepository(LactafarmaContext context) : base(context)
        {
            User = new User
            {
                LanguageId = Guid.Parse("7C0AFE0E-0B25-4AEA-8AAE-51CBDDE1B134")
            };
        }

        #endregion

        #region Public Methods

        public IEnumerable<DrugMultilingual> GetDrugsByGroup(int groupId)
        {
            return EntityContext.DrugsMultilingual.Where(l => l.LanguageId == User.LanguageId).Include(d => d.Drug)
                .ThenInclude(g => g.Group).Where(gm => gm.Drug.GroupId == groupId).AsEnumerable();
        }

        public IEnumerable<DrugBrand> GetDrugsByBrand(int brandId)
        {
            return EntityContext.DrugBrands.Where(db => db.BrandId == brandId).Include(d => d.Drug)
                .ThenInclude(dm => dm.DrugsMultilingual)
                .Where(dm => dm.Drug.DrugsMultilingual.FirstOrDefault().LanguageId == User.LanguageId).AsEnumerable();
        }

        public DrugMultilingual GetDrug(int drugId)
        {
            return EntityContext.DrugsMultilingual.Where(e => e.DrugId == drugId && e.LanguageId == User.LanguageId)
                .Include(e => e.Drug).FirstOrDefault();
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