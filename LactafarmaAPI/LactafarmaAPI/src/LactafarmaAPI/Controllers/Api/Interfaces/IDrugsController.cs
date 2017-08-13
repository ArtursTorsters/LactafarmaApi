using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LactafarmaAPI.Controllers.Api.Interfaces
{
    interface IDrugsController
    {
        JsonResult GetDrugsByName(string startsWith);
        JsonResult GetDrugsByGroup(int groupId);
        JsonResult GetDrugsByBrand(int brandId);
        JsonResult GetDrugByAlias(int aliasId);
        JsonResult GetDrug(int drugId);

    }
}
