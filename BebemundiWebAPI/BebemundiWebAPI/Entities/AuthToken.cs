using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebemundiWebAPI.Entities
{
  public class AuthToken
  {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public decimal Id { get; set; }
      public string Token { get; set; }
      public DateTime Expiration { get; set; }
      public decimal UserId { get; set; }
  }
}
