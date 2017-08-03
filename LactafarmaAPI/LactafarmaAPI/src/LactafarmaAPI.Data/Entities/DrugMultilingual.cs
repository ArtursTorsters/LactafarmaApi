using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class DrugMultilingual
    {
        public long DrugId { get; set; }
        public string Name { get; set; }
        public string Risk { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public Drug Drug { get; set; }
        public Guid LanguageId { get; set; }

        public Language Language { get; set; }

    }
}
