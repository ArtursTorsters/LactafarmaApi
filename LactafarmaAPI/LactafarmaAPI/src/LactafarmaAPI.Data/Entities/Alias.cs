using System;
using System.Collections.Generic;

namespace LactafarmaAPI.Data.Entities
{
    public class Alias
    {
        public long Id { get; set; }
        public long DrugId { get; set; }
        public Drug Drug { get; set; }
        public virtual ICollection<AliasMultilingual> AliasMultilingual { get; set; }

    }
}