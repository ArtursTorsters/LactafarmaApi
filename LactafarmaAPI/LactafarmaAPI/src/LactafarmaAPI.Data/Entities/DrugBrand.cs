using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LactafarmaAPI.Data.Entities
{
    public class DrugBrand
    {
        public int DrugId { get; set; }
        public int BrandId { get; set; }


        //Navigation Properties
        public Brand Brand { get; set; }
        public Drug Drug { get; set; }

    }
}
