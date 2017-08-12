using LactafarmaAPI.Core;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.Logging;

namespace LactafarmaAPI.Data.Repositories
{
    public class AlertsRepository : DataRepositoryBase<Alert, LactafarmaContext, User>, IAlertRepository
    {
        private readonly ILogger<AlertsRepository> _logger;

        public AlertsRepository(LactafarmaContext context, ILogger<AlertsRepository> logger): base(context)
        {
            User = new User()
            {
                LanguageId = Guid.Parse("7C0AFE0E-0B25-4AEA-8AAE-51CBDDE1B134")
            };

            _logger = logger;
        }

        public IEnumerable<AlertMultilingual> GetAllAlerts()
        {
            try
            {
                return EntityContext.AlertsMultilingual.Where(l => l.LanguageId == User.LanguageId)
                    .Include(e => e.Alert)
                    .ThenInclude(e => e.Drug)
                    .AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllAlerts with message: {ex.Message}");
                return null;
            }
        }

        public IEnumerable<AlertMultilingual> GetAlertsByDrug(int drugId)
        {
            try
            {
                return EntityContext.AlertsMultilingual.Where(l => l.LanguageId == User.LanguageId)
                    .Include(d => d.Alert).ThenInclude(d => d.Drug).Where(d => d.Alert.Drug.Id == drugId)
                    .AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAlertsByDrug with message: {ex.Message}");
                return null;
            }
        }

        protected override Expression<Func<Alert, bool>> IdentifierPredicate(int id)
        {
            return (e => e.Id == id);
        }
    }
}
