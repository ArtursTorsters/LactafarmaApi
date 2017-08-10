using System;
using System.Collections.Generic;
using System.Text;

namespace LactafarmaAPI.Domain.Models
{
    public class Group
    {
        public int Id { get; set; }
        public DateTime Modified { get; set; }
        public string Name { get; set; }
        public Language Language { get; set; }
        public IEnumerable<Drug> Drugs { get; set; }
    }
}
