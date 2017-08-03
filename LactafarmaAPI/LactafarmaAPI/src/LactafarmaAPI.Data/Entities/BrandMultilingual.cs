using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class BrandMultilingual
    {
        public long BrandId { get; set; }
        public string Name { get; set; }
        public Guid LanguageId { get; set; }

        public Brand Brand { get; set; }
        public Language Language { get; set; }

    }
}
