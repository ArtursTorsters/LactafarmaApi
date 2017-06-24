using BebemundiWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BebemundiWebAPI.Models
{
    public class GroupModel
    {        
        public string Url { get; set; }
        public Group Group { get; set; }
    }
}