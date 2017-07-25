using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class Drug
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Risk { get; set; }
        public DateTime Modified { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public long GroupId { get; set; }
        public Group Group { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
        public virtual ICollection<Alias> Aliases { get; set; }
        public virtual ICollection<Drug> DrugAlternatives { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
    }
}
