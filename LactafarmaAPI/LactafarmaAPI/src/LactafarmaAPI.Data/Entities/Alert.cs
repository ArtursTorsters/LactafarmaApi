using LactafarmaAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class Alert: IIdentifiableEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int DrugId { get; set; }

        //Navigation Properties
        public Drug Drug { get; set; }
        public virtual ICollection<AlertMultilingual> AlertsMultilingual { get; set; }
        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
