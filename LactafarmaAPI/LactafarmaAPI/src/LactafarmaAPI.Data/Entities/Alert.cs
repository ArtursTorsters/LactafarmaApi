using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class Alert
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public long DrugId { get; set; }
        public Drug Drug { get; set; }
        public virtual ICollection<AlertMultilingual> AlertsMultilingual { get; set; }

    }
}
