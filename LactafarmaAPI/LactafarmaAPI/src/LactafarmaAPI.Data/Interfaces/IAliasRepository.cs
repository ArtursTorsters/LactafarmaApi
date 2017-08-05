using LactafarmaAPI.Core.Interfaces;
using LactafarmaAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LactafarmaAPI.Data.Interfaces
{
    public interface IAliasRepository : IDataRepository<Alias>
    {
        IEnumerable<Alias> GetAliasesByDrug(int drugId);

    }
}
