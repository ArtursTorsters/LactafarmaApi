using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LactafarmaAPI.Controllers.Api.Interfaces
{
    interface IAlertsController
    {
        JsonResult GetAlertsByDrug(int drugId);
    }
}
