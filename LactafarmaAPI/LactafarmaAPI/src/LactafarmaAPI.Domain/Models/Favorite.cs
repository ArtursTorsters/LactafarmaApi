using System;
using System.Collections.Generic;
using System.Text;
using LactafarmaAPI.Domain.Models.Base;

namespace LactafarmaAPI.Domain.Models
{
    public class Favorite: BaseModel
    {
        //Navigation Properties
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
