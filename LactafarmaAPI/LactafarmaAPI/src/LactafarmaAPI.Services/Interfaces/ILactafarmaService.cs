using System;
using System.Collections.Generic;
using LactafarmaAPI.Domain.Models;
using LactafarmaAPI.Domain.Models.Base;

namespace LactafarmaAPI.Services.Interfaces
{
    public interface ILactafarmaService
    {
        #region Public Methods

        IEnumerable<Alert> GetAlertsByDrug(int drugId);
        IEnumerable<BaseModel> GetAllAlerts();
        IEnumerable<BaseModel> GetAllAliases();
        IEnumerable<Alias> GetAliasesByDrug(int drugId);
        IEnumerable<Brand> GetBrandsByDrug(int drugId);
        IEnumerable<BaseModel> GetAllBrands();
        IEnumerable<BaseModel> GetAllDrugs();
        IEnumerable<Drug> GetDrugsByGroup(int groupId);
        IEnumerable<Drug> GetDrugsByBrand(int brandId);
        IEnumerable<BaseModel> GetAllGroups();
        Group GetGroup(int groupId);
        User GetUser(Guid userId);
        Alias GetAlias(int aliasId);
        Brand GetBrand(int brandId);
        Drug GetDrug(int drugId);
        Drug GetDrugByAlias(int aliasId);

        #endregion
    }
}