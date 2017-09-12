using System;
using System.Collections.Generic;
using System.Text;
using LactafarmaAPI.Domain.Models.Base;

namespace LactafarmaAPI.Domain.Models
{
    public class Alert: BaseModel
    {
        public DateTime Created { get; set; }
        public string OldRisk { get; set; }
        public string NewRisk { get; set; }
    }
}
