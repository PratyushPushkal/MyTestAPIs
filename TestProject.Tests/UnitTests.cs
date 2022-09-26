using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Data;
using TestProject.WebAPI.Models;
using Xunit;

namespace TestProject.Tests
{
  public class UnitTests
  {
      private IUserData _userdata;
      private List<User> MockUser()
      { 
        List<User> users = new List<User>();
        User user = new User() {
          Email = "P@P.com",
          Expenses = 10000,
          Salary = 20000,
          Name = "Pratyush"
        };
        users.Add(user);
        return users;
      }
      [Fact]
      public void CreateUser()
      {
          List<User> users = MockUser();
          _userdata.CreateUser(users[0]);
          Assert.True(false, "Create a test with an assertion");
      }
    }
}