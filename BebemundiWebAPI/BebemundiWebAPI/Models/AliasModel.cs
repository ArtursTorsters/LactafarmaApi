using BebemundiWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BebemundiWebAPI.Models
{
    public class AliasModel
    {       
        public string Url { get; set; }
        public Alias Alias { get; set; }
    }
}