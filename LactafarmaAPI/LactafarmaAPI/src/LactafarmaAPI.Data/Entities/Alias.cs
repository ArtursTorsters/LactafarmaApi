using LactafarmaAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LactafarmaAPI.Data.Entities
{
    public class Alias : IIdentifiableEntity
    {
        public int Id { get; set; }
        public int DrugId { get; set; }

        //Navigation Properties
        public Drug Drug { get; set; }
        public virtual ICollection<AliasMultilingual> AliasMultilingual { get; set; }

        public int EntityId
        {
            get { return Id; }
            set { Id = value; }
        }

    }
}