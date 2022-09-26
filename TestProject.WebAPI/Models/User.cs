using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.WebAPI.Models
{
  public class User
  {
    [Key]
    public int UserId { get; set; }
    [Required(ErrorMessage = "Name is Mandatory")]
    [MaxLength(50,ErrorMessage =" Name can't be more than 50 Character")]
    public string Name { get; set; }
    [Required(ErrorMessage ="Email is Mandatory")]
    [EmailAddress(ErrorMessage = "Provide Correct Email")]
    public string Email { get; set; }
    [Required(ErrorMessage ="Provide Salary")]
    [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public decimal Salary { get; set; }
    [Required(ErrorMessage ="Provide Expenses")]
    [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
    public decimal Expenses { get; set; }
  }
}
