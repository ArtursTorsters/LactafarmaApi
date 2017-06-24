using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BebemundiWebAPI.Entities
{
    public class ProductTrademark
    {     
        [Key]
        public string IdMarca { get; set; }
        public string IdMedicamento { get; set; }
    }
}