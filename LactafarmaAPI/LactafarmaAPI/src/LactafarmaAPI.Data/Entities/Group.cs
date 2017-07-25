using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class Group
    {
        public long Id { get; set; }
        public DateTime Modified { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}
