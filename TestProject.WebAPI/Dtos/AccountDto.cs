using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.WebAPI.Dtos
{
  public class AccountDto
  {
    public string Email { get; set; }
    public string Password { get; set; }

  }

  public class AccountUpdateDto : AccountDto
  {
    public int AccountId { get; set; }
  }

  public class AccountCreateDto : AccountDto
  {
    public bool isActive { get; set; }
  }
}
