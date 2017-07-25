using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class Token
    {
        public Guid Id { get; set; }
        public string Hash { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
