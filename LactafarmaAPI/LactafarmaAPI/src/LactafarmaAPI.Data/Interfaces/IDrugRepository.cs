using LactafarmaAPI.Core.Interfaces;
using LactafarmaAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LactafarmaAPI.Data.Interfaces
{
    public interface IDrugRepository : IDataRepository<Drug>
    {
        IEnumerable<Drug> GetDrugsByGroup(int groupId);
        IEnumerable<Drug> GetDrugsByBrand(int brandId);

        Drug GetDrug(int drugId);

    }
}
