using System;
using System.Collections.Generic;
using LactafarmaAPI.Domain.Models;

namespace LactafarmaAPI.Services.Interfaces
{
    public interface ILactafarmaService
    {
        #region Public Methods

        IEnumerable<Alert> GetAlertsByDrug(int drugId);
        IEnumerable<Alert> GetAllAlerts();
        IEnumerable<Alias> GetAllAliases();
        IEnumerable<Alias> GetAliasesByDrug(int drugId);
        IEnumerable<Brand> GetBrandsByDrug(int drugId);
        IEnumerable<Brand> GetAllBrands();
        IEnumerable<Drug> GetAllDrugs();
        IEnumerable<Drug> GetDrugsByGroup(int groupId);
        IEnumerable<Drug> GetDrugsByBrand(int brandId);
        IEnumerable<Group> GetAllGroups();
        Group GetGroup(int groupId);
        User GetUser(Guid userId);
        Alias GetAlias(int aliasId);
        Brand GetBrand(int brandId);
        Drug GetDrug(int drugId);
        Drug GetDrugByAlias(int aliasId);

        #endregion
    }
}