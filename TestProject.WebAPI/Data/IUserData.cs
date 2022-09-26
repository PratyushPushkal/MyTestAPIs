using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.WebAPI.Dtos;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Data
{
  public interface IUserData
  {
    Task<List<User>> ListUsers();
    Task<bool> CreateUser(User user);
    Task<User> GetUser(int UserId);
  }
}
