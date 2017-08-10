using System;
using System.Collections.Generic;
using System.Text;

namespace LactafarmaAPI.Domain.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public Drug Drug { get; set; }
        public string Name { get; set; }
        public string Risk { get; set; }
        public string Comment { get; set; }
        public Language Language { get; set; }
    }
}
