using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LactafarmaAPI.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string TwitterInfo { get; set; }
        public string FacebookInfo { get; set; }
        public string GoogleInfo { get; set; }
        public string SecretKey { get; set; }
        public string AppId { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
        public Guid LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
