using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class Drug
    {
        public int Id { get; set; }
        public DateTime Modified { get; set; }
        public int GroupId { get; set; }

        //Navigation Properties
        public Group Group { get; set; }
        public virtual ICollection<Alert> Alerts { get; set; }
        public virtual ICollection<Alias> Aliases { get; set; }
        public virtual ICollection<DrugAlternative> DrugAlternatives { get; set; }
        public virtual ICollection<DrugBrand> DrugBrands { get; set; }
        public virtual ICollection<DrugMultilingual> DrugsMultilingual { get; set; }

    }
}
