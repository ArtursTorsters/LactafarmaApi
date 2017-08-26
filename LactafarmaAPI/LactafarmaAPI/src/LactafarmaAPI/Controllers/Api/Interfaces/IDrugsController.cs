using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LactafarmaAPI.Domain.Models;

namespace LactafarmaAPI.Controllers.Api.Interfaces
{
    interface IDrugsController
    {
        IEnumerable<Drug> GetDrugsByName(string startsWith);
        IEnumerable<Drug> GetDrugsByGroup(int groupId);
        IEnumerable<Drug> GetDrugsByBrand(int brandId);
        Drug GetDrugByAlias(int aliasId);
        Drug GetDrug(int drugId);
    }
}
