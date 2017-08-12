using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LactafarmaAPI.Core;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LactafarmaAPI.Data.Repositories
{
    public class GroupsRepository : DataRepositoryBase<Group, LactafarmaContext, User>, IGroupRepository
    {
        #region Constructors

        public GroupsRepository(LactafarmaContext context) : base(context)
        {
            User = new User
            {
                LanguageId = Guid.Parse("7C0AFE0E-0B25-4AEA-8AAE-51CBDDE1B134")
            };
        }

        #endregion

        #region Public Methods

        public IEnumerable<Group> GetAllGroups()
        {
            return EntityContext.Groups
                .Include(e => e.GroupsMultilingual.Where(l => l.LanguageId == User.LanguageId))
                .AsEnumerable();
        }

        public GroupMultilingual GetGroup(int groupId)
        {
            return EntityContext.GroupsMultilingual.Where(gm => gm.LanguageId == User.LanguageId).Include(g => g.Group)
                .Single(g => g.GroupId == groupId);
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