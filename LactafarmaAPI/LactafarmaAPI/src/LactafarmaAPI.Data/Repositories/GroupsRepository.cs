using LactafarmaAPI.Core;
using LactafarmaAPI.Data.Entities;
using LactafarmaAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LactafarmaAPI.Data.Repositories
{
    public class GroupsRepository : DataRepositoryBase<Group, LactafarmaContext, User>, IGroupRepository
    {
        public GroupsRepository(LactafarmaContext context, User user) : base(context, user)
        {
        }

        public IEnumerable<Group> GetAllGroups()
        {
            return EntityContext.Groups
             .Include(e => e.GroupsMultilingual.Where(l => l.LanguageId == User.LanguageId))
             .AsEnumerable();
        }

        protected override Expression<Func<Group, bool>> IdentifierPredicate(int id)
        {
            return (e => e.Id == id);
        }
    }
}
