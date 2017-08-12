using LactafarmaAPI.Core.Interfaces;
using LactafarmaAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LactafarmaAPI.Data.Interfaces
{
    public interface IDrugRepository : IDataRepository<Drug>
    {
        IEnumerable<DrugMultilingual> GetDrugsByGroup(int groupId);
        IEnumerable<DrugBrand> GetDrugsByBrand(int brandId);
        DrugMultilingual GetDrug(int drugId);

    }
}
