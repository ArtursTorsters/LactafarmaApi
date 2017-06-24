using BebemundiWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BebemundiWebAPI.Models
{
    public class TrademarkModel
    {        
        public string Url { get; set; }
        public Trademark Trademark { get; set; }
    }
}