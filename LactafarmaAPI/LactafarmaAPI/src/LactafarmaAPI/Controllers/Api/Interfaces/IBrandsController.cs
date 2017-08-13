using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LactafarmaAPI.Controllers.Api.Interfaces
{
    interface IBrandsController
    {
        JsonResult GetBrandsByName(string startsWith);
        JsonResult GetBrandsByDrug(int drugId);
        JsonResult GetBrand(int brandId);

    }
}
