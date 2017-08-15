using LactafarmaAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LactafarmaAPI.Data.Entities
{
    public class User : IdentityUser, IIdentifiableGuidEntity
    {
        public string Name { get; set; }
        public string TwitterInfo { get; set; }
        public string FacebookInfo { get; set; }
        public string GoogleInfo { get; set; }
        public string SecretKey { get; set; }
        public string AppId { get; set; }
        public Guid LanguageId { get; set; }

        //Navigation Properties
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
        public Language Language { get; set; }

        public string EntityId
        {
            get => Id;
            set => Id = value;
        }
    }
}
