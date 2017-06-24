using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BebemundiWebAPI.Entities
{
    public class Product
    {        
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Fecha { get; set; }
        public string Riesgo { get; set; }
        public string DescripcionRiesgo { get; set; }
        public string Comentario { get; set; }
        public string IdGrupo { get; set; }
      
    }
}