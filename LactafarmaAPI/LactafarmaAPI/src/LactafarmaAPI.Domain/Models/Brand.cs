using System;
using System.Collections.Generic;
using System.Text;
using LactafarmaAPI.Domain.Models.Base;

namespace LactafarmaAPI.Domain.Models
{
    public class Brand : BaseModel
    {
        public List<Drug> Drugs { get; set; }
        public Language Language { get; set; }

    }
}
