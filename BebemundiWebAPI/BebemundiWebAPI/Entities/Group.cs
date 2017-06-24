using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BebemundiWebAPI.Entities
{
    public class Group
    {        
        public string Id { get; set; }
        public string Fecha { get; set; }
        public string Nombre { get; set; }        
    }
}