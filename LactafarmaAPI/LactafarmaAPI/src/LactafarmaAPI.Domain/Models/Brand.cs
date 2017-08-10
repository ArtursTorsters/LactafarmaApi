using System;
using System.Collections.Generic;
using System.Text;

namespace LactafarmaAPI.Domain.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Drug> Drugs { get; set; }
        public Language Language { get; set; }

    }
}
