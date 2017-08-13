using System;
using System.Collections.Generic;
using System.Text;
using LactafarmaAPI.Domain.Models.Base;

namespace LactafarmaAPI.Domain.Models
{
    public class Drug : BaseModel
    {
        public DateTime Modified { get; set; }
        public string Risk { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public Language Language { get; set; }
        public Group Group { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Drug> Alternatives { get; set; }
        public IEnumerable<Alias> Aliases { get; set; }
    }
}
