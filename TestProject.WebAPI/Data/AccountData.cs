using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.WebAPI.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace TestProject.WebAPI.Data
{
  public class AccountData : IAccountData
  {
    private ApplicationContext _context;
    public AccountData(ApplicationContext context)
    {
      _context = context;
    }

    #region Read
    public async Task<Account> GetAccountById(int accId)
    {
      var result = await _context.Accounts.FirstOrDefaultAsync(item => item.AccountId == accId);
      if (result == null)
      {
        throw new RestException(System.Net.HttpStatusCode.NotFound, new { Message = "Account not found." });
      }
      return result;
    }

    public async Task<List<Account>> ListAccount()
    {
      try
      {
        var res = await _context.Accounts.ToListAsync();
        return res;
      }
      catch (Exception)
      {
        throw;
      }
    }
    public async Task<bool> Login(Account account)
    {
      try
      {
        var res = await _context.Accounts.FirstOrDefaultAsync(item => item.Email == account.Email);
        if(res == null)
        {
          throw new RestException(System.Net.HttpStatusCode.NotFound, new { Message = "Account not found." });
        }
        if (res.isActive)
        {
          string password = Common.GetHashedPassword(account.Password);
          if (res.Password.Equals(password))
          {
            return true;
          }
          else
          {
            return false;
          }
        }
        else
        {
          return false;
        }
        
      }
      catch (Exception)
      {
        throw;
      }
    }
    #endregion

    #region CUD
    public async Task<bool> CreateAccount(Account account)
    {
      try
      {
        var dupUser = await _context.Accounts.FirstOrDefaultAsync(item => item.Email == account.Email && item.AccountId != account.AccountId);
        if (dupUser != null)
        {
          throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Message = "Email is already used with different Account." });
        }

        var UserCheck = await _context.Users.FirstOrDefaultAsync(item => item.Email == account.Email);
        if (UserCheck == null)
        {
          throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Message = "User not available for given Email" });
        }
        account.Password = Common.GetHashedPassword(account.Password);
        _context.Accounts.Add(account);
        var res = await _context.SaveChangesAsync();
        return res > 0;
      }
      catch (Exception)
      {
        throw;
      }
    }
    public async Task<bool> DeleteAccountById(int accId)
    {
      try
      {
        var dupUser = await _context.Accounts.FirstOrDefaultAsync(item => item.AccountId == accId);
        if (dupUser == null)
        {
          throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Message = "Account not found." });
        }
        _context.Accounts.Remove(dupUser);
        var res = await _context.SaveChangesAsync();
        return res > 0;
      }
      catch (Exception)
      {
        throw;
      }
    }
    public async Task<bool> UpdatePassword(Account account)
    {
      try
      {
        var existing = await _context.Accounts.FirstOrDefaultAsync(item => item.Email == account.Email);
        if (existing == null)
        {
          throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Message = "No Account with given Email present to update." });
        }
        else
        {
          existing.Password = Common.GetHashedPassword(account.Password);
        }
        var res = await _context.SaveChangesAsync();
        return res > 0;
      }
      catch (Exception)
      {
        throw;
      }
    }
    #endregion
  }
}
