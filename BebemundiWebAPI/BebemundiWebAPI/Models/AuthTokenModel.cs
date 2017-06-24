using BebemundiWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BebemundiWebAPI.Models
{
    public class AuthTokenModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}