using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LactafarmaAPI.Core.Interfaces;
using LactafarmaAPI.Data.PagedData;
using LactafarmaAPI.Domain.Models;
using LactafarmaAPI.Domain.Models.Base;
using Log = LactafarmaAPI.Domain.Models.Log;
using User = LactafarmaAPI.Data.Entities.User;

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
        //User GetUser(string userId);
        Alias GetAlias(int aliasId);
        Brand GetBrand(int brandId);
        Drug GetDrug(int drugId);
        Drug GetDrugByAlias(int aliasId);

        #endregion

        Task<IList<string>> GetLevelsAsync();

        Task<IPagedList<Log>> GetLogsAsync(LogPagedDataRequest request);
        void SetUser(User currentUser);
    }
}