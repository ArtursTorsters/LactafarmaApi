using LactafarmaAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class Favorite : IIdentifiableGuidEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int DrugId { get; set; }
        public Guid UserId { get; set; }

        //Navigation Properties
        public Drug Drug { get; set; }
        public User User { get; set; }

        public string EntityId
        {
            get => Id;
            set => Id = value;
        }
    }
}
