using System;
using System.Collections.Generic;
using System.Text;

namespace LactafarmaAPI.Domain.Models
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //Navigation Properties
        public Drug Drug { get; set; }
        public User User { get; set; }
    }
}
