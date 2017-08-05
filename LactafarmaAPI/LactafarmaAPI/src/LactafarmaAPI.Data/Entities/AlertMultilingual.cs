using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class AlertMultilingual
    {
        public int AlertId { get; set; }
        public string Name { get; set; }
        public string Risk { get; set; }
        public string Comment { get; set; }
        public Guid LanguageId { get; set; }

        //Navigation Properties
        public Alert Alert { get; set; }
        public Language Language { get; set; }        
    }
}
