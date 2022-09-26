using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Data
{
  public interface IAccountData
  {
    Task<List<Account>> ListAccount();
    Task<bool> CreateAccount(Account account);
    Task<bool> DeleteAccountById(int accId);
    Task<bool> UpdatePassword(Account account);
    Task<Account> GetAccountById(int accId);
    Task<bool> Login(Account account);
  }
}
