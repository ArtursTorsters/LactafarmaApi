using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class Brand
    {
        public long Id { get; set; }
        public virtual ICollection<DrugBrand> DrugBrands { get; set; }
        public virtual ICollection<BrandMultilingual> BrandsMultilingual { get; set; }

    }
}
