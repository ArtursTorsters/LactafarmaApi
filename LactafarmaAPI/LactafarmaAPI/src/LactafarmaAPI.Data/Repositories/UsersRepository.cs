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
    public class UsersRepository : DataGuidRepositoryBase<User, LactafarmaContext, User>, IUserRepository
    {
        private readonly ILogger<UsersRepository> _logger;

        #region Constructors

        public UsersRepository(LactafarmaContext context, ILogger<UsersRepository> logger) : base(context)
        {
            User = new User
            {
                LanguageId = Guid.Parse("7C0AFE0E-0B25-4AEA-8AAE-51CBDDE1B134")
            };
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return EntityContext.Users
                    .Include(e => e.Language).Where(l => l.LanguageId == User.LanguageId)
                    .AsEnumerable();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetAllUsers with message: {ex.Message}");
                return null;
            }
        }

        public User GetUser(string userId)
        {
            try
            {
                return EntityContext.Users
                    .Where(x => x.Id == userId).Include(e => e.Language)
                    .FirstOrDefault(l => l.LanguageId == User.LanguageId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception on GetUser with message: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Overridden Members

        protected override Expression<Func<User, bool>> IdentifierPredicate(string id)
        {
            return e => e.Id == id;
        }

        #endregion
    }
}