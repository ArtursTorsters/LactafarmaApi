using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LactafarmaAPI.Core;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LactafarmaAPI.Data.Repositories
{
    public class GroupsRepository : DataRepositoryBase<Group, LactafarmaContext, User>, IGroupRepository
    {
        private readonly ILogger<GroupsRepository> _logger;

        #region Constructors

        public GroupsRepository(LactafarmaContext context, ILogger<GroupsRepository> logger) : base(context)
        {
            User = new User
            {
                LanguageId = Guid.Parse("7C0AFE0E-0B25-4AEA-8AAE-51CBDDE1B134")
            };

            _logger = logger;
        }

        #endregion

        #region Public Methods

        public IEnumerable<GroupMultilingual> GetAllGroups()
        {
            try
            {
                return EntityContext.GroupsMultilingual.Where(l => l.LanguageId == User.LanguageId)
                    .Include(g => g.Group)
                    .AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllGroups with message: {ex.Message}");
                return null;
            }
        }

        public GroupMultilingual GetGroup(int groupId)
        {
            try
            {
                return EntityContext.GroupsMultilingual.Where(gm => gm.LanguageId == User.LanguageId).Include(g => g.Group)
                    .Single(g => g.GroupId == groupId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetGroup with message: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Overridden Members

        protected override Expression<Func<Group, bool>> IdentifierPredicate(int id)
        {
            return e => e.Id == id;
        }

        #endregion
    }
}