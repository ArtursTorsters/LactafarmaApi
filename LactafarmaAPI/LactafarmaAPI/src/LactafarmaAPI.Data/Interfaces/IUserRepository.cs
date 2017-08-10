using LactafarmaAPI.Core.Interfaces;
using LactafarmaAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LactafarmaAPI.Data.Interfaces
{
    public interface IUserRepository : IDataGuidRepository<User>
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(Guid userId);
    }
}
