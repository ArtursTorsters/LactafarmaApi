using System;
using System.Collections.Generic;
using System.Text;
using LactafarmaAPI.Domain.Models.Base;

namespace LactafarmaAPI.Domain.Models
{
    public class Product : BaseModel
    {
        public string Description { get; set; }
        public DateTime Modified { get; set; }
        public Risk Risk { get; set; }
        public Language Language { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Product> Alternatives { get; set; }
        public IEnumerable<Alias> Aliases { get; set; }
    }
}
