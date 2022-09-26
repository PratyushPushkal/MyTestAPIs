using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.WebAPI.Models
{
  public class Account
  {
    [Key]
    public int AccountId { get; set; }
    [Required(ErrorMessage = "Provide Email Id")]
    [EmailAddress(ErrorMessage = "Provide Correct Email")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Provide Password")]
    [MinLength(5, ErrorMessage = " Provide atleast 5 character")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Provide Either Account is Active or not")]
    public bool isActive { get; set; }

  }
}
