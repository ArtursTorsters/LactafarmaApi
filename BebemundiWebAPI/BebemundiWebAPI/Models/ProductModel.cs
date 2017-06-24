using BebemundiWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BebemundiWebAPI.Models
{
    public class ProductModel
    {        
        public string Url { get; set; }
        public Product Product { get; set; }
      
    }
}