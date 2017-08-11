using LactafarmaAPI.Core;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LactafarmaAPI.Data.Repositories
{
    public class AlertsRepository : DataRepositoryBase<Alert, LactafarmaContext, User>, IAlertRepository
    {
        public AlertsRepository(LactafarmaContext context): base(context)
        {
            User = new User()
            {
                LanguageId = Guid.Parse("7C0AFE0E-0B25-4AEA-8AAE-51CBDDE1B134")
            };
        }

        public IEnumerable<Alert> GetAllAlerts()
        {
            return EntityContext.Alerts
                .Include(e => e.AlertsMultilingual.Where(l => l.LanguageId == User.LanguageId))
                .Include(e => e.Drug)
                .AsEnumerable();
        }

        public IEnumerable<Alert> GetAlertsByDrug(int drugId)
        {
            return EntityContext.Alerts.Where(e => e.DrugId == drugId)                
                .Include(e => e.AlertsMultilingual.Where(l => l.LanguageId == User.LanguageId))
                .AsEnumerable();
        }

        protected override Expression<Func<Alert, bool>> IdentifierPredicate(int id)
        {
            return (e => e.Id == id);
        }
    }
}
