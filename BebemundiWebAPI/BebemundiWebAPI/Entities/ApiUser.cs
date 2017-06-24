using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebemundiWebAPI.Entities
{
  public class ApiUser
  {
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    public decimal UserId { get; set; }    
    public string Email { get; set; }
    public string Secret { get; set; }
    public string AppId { get; set; }
    public string Password { get; set; }    
  }
}
