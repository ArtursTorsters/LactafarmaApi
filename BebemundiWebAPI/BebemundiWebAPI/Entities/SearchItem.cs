using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BebemundiWebAPI.Entities
{
    public class SearchItem
    {        
        public string Id { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public string DescripcionTipo { get; set; }
    }
}