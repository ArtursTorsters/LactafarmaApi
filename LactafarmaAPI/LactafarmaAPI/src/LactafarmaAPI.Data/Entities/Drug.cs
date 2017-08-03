using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class Drug
    {
        public long Id { get; set; }
        public DateTime Modified { get; set; }
        public long GroupId { get; set; }
        public Group Group { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
        public virtual ICollection<Alias> Aliases { get; set; }
        public virtual ICollection<DrugAlternative> DrugAlternatives { get; set; }
        public virtual ICollection<DrugBrand> DrugBrands { get; set; }
        public virtual ICollection<DrugMultilingual> DrugsMultilingual { get; set; }

    }
}
