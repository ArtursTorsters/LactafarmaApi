using System;
using System.Collections.Generic;
using System.Text;
using LactafarmaAPI.Domain.Models.Base;

namespace LactafarmaAPI.Domain.Models
{
    public class Alias : BaseModel
    {
        public Drug Drug { get; set; }
        public Language Language { get; set; }
    }
}
