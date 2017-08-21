using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LactafarmaAPI.Data.Entities
{
    public class DrugAlternative
    {
        public int DrugId { get; set; }
        public int DrugAlternativeId { get; set; }
    }
}
