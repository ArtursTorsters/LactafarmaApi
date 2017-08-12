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
    public class UsersRepository : DataGuidRepositoryBase<User, LactafarmaContext, User>, IUserRepository
    {
        #region Constructors

        public UsersRepository(LactafarmaContext context) : base(context)
        {
            User = new User
            {
                LanguageId = Guid.Parse("7C0AFE0E-0B25-4AEA-8AAE-51CBDDE1B134")
            };
        }

        #endregion

        #region Public Methods

        public IEnumerable<User> GetAllUsers()
        {
            return EntityContext.Users
                .Include(e => e.Language).Where(l => l.LanguageId == User.LanguageId)
                .AsEnumerable();
        }

        public User GetUser(Guid userId)
        {
            return EntityContext.Users
                .Where(x => x.Id == userId).Include(e => e.Language)
                .FirstOrDefault(l => l.LanguageId == User.LanguageId);
        }

        #endregion

        #region Overridden Members

        protected override Expression<Func<User, bool>> IdentifierPredicate(Guid id)
        {
            return e => e.Id == id;
        }

        #endregion
    }
}