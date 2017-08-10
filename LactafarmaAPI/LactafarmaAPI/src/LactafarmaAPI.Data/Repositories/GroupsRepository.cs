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

        public Group GetGroup(int groupId)
        {
            return EntityContext.Groups.Where(x => x.Id == groupId)
                .Include(e => e.GroupsMultilingual.Where(l => l.LanguageId == User.LanguageId)).Include(x => x.Drugs)
                .FirstOrDefault();
        }

        protected override Expression<Func<Group, bool>> IdentifierPredicate(int id)
        {
            return (e => e.Id == id);
        }
    }
}
