using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace BebemundiWebAPI.Services
{
    public class BebemundiWebAPIIdentityService : IBebemundiWebAPIIdentityService
    {
        public string CurrentUser
        {
            get
            {
                return Thread.CurrentPrincipal.Identity.Name;
            }
        }
    }
}