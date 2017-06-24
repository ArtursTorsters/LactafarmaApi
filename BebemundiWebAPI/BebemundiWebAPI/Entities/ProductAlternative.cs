using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BebemundiWebAPI.Entities
{
    public class ProductAlternative
    {
        [Key]
        public string IdMedicamento { get; set; }
        public string IdAlternativaMedicamento { get; set; }
    }
}