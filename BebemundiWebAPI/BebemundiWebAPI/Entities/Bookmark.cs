using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BebemundiWebAPI.Entities
{
    public class Bookmark
    {
        [Key]
        public string Id { get; set; }
        public string IdMedicamento { get; set; }
        public string IdUsuario { get; set; }
    }
}