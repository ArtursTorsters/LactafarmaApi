using System;
using System.Collections.Generic;
using System.Text;

namespace LactafarmaAPI.Data.Entities
{
    public class DrugAlternative
    {
        public long DrugId { get; set; }
        public long DrugAlternativeId { get; set; }
        public Drug Drug { get; set; }
        public Drug DrugOption { get; set; }
    }
}
