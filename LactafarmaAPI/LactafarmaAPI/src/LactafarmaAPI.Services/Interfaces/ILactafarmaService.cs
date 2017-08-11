using System;
using System.Collections.Generic;
using LactafarmaAPI.Domain.Models;

namespace LactafarmaAPI.Services.Interfaces
{
    public interface ILactafarmaService
    {
        #region Public Methods

        IEnumerable<Alert> GetAlertsByDrug(int drugId);
        IEnumerable<Alias> GetAliasesByDrug(int drugId);
        IEnumerable<Brand> GetBrandsByDrug(int drugId);
        IEnumerable<Drug> GetDrugsByGroup(int groupId);
        IEnumerable<Drug> GetDrugsByBrand(int brandId);
        Group GetGroup(int groupId);
        User GetUser(Guid userId);
        Alias GetAlias(int aliasId);
        Brand GetBrand(int brandId);
        Drug GetDrug(int drugId);
        Drug GetDrugByAlias(int aliasId);

        #endregion
    }
}