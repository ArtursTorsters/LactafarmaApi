using System;
using System.Collections.Generic;
using System.Text;

namespace LactafarmaAPI.Domain.Models
{
    public class Alias
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Drug Drug { get; set; }
        public Language Language { get; set; }
    }
}
