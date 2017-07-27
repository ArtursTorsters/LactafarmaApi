using System;
using System.Collections.Generic;
using System.Text;

namespace LactafarmaAPI.Data.Entities
{
    public class DrugBrand
    {
        public long DrugId { get; set; }
        public Drug Drug { get; set; }
        public long BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
