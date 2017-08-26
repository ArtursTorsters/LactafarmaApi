using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LactafarmaAPI.Domain.Models;

namespace LactafarmaAPI.Controllers.Api.Interfaces
{
    interface IGroupsController
    {
        IEnumerable<Group> GetGroupsByName(string startsWith);
        Group GetGroup(int groupId);

    }
}
