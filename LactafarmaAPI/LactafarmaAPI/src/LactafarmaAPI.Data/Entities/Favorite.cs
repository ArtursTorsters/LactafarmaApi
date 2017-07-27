using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public long DrugId { get; set; }
        public Guid UserId { get; set; }
        public Drug Drug { get; set; }
        public User User { get; set; }
    }
}
