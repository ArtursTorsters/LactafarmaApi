using System;
using System.Collections.Generic;
using System.Text;
using LactafarmaAPI.Domain.Models.Base;

namespace LactafarmaAPI.Domain.Models
{
    public class Group : BaseModel
    {
        public DateTime Modified { get; set; }
        public Language Language { get; set; }
        public IEnumerable<Drug> Drugs { get; set; }
    }
}
