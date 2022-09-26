using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.WebAPI.Dtos;
using TestProject.WebAPI.Models;
using AutoMapper;
using System.Threading;

namespace TestProject.WebAPI.Data
{
  public class UserData : IUserData
  {
    private ApplicationContext _context;
    public UserData(ApplicationContext context)
    {
      _context = context;
    }
    public async Task<List<User>> ListUsers()
    {
      try
      {
        var res = await _context.Users.ToListAsync();
        return res;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<bool> CreateUser(User user)
    {
      try
      {
        var dupUser = await _context.Users.FirstOrDefaultAsync(item => item.Email == user.Email && item.UserId != user.UserId);
        if (dupUser != null)
        {
          throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Message = "Email is already used with different user." });
        }
        if ((user.Salary - user.Expenses) < 1000)
        {
          throw new RestException(System.Net.HttpStatusCode.BadRequest, new { Message = "`monthly salary - monthly expenses` is less than $1000, Can't create user." });
        }
        _context.Users.Add(user);
        var res = await _context.SaveChangesAsync();
        return res > 0;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<User> GetUser(int UserId)
    {
      var result = await _context.Users.FirstOrDefaultAsync(item => item.UserId == UserId);
      if (result == null)
      {
        throw new RestException(System.Net.HttpStatusCode.NotFound, new { Message = "User not found." });
      }
      return result;
    }
  }
}
