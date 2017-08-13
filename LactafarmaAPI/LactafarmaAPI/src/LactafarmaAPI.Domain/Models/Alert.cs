using System;
using System.Collections.Generic;
using System.Text;
using LactafarmaAPI.Domain.Models.Base;

namespace LactafarmaAPI.Domain.Models
{
    public class Alert: BaseModel
    {
        public DateTime Created { get; set; }
        public Drug Drug { get; set; }
        public string Risk { get; set; }
        public string Comment { get; set; }
        public Language Language { get; set; }
    }
}
