using System;
using System.Collections.Generic;
using System.Text;
using TestProject.WebAPI.Models;

namespace TestProject.Tests.TestCases
{
  public static class MockData
  {
    public static User GetUser()
    {
      return new User() {
        Email = "P@P.com",
        Expenses = 10000,
        Salary = 20000,
        Name = "Pratyush"
      };
    }
    public static List<User> MockUserData()
    {
      List<User> users = new List<User>() {
        new User {
        Email = "P@P.com",
        Expenses = 10000,
        Salary = 20000,
        Name = "Pratyush"
      }, new User{
        Email = "Q@P.com",
        Expenses = 10000,
        Salary = 20000,
        Name = "Pragyan"
      }};
      return users;
    }

    public static List<User> MockUserEmptyData()
    {
      return new List<User>();
    }

    public static Account GetAccount()
    {
      return new Account()
      {
        Email = "P@P.com",
        Password = "123456",
        isActive = true
      };
    }
    public static List<Account> MockAccountData()
    {
      List<Account> accounts = new List<Account>() { new Account()
      {
        Email = "P@P.com",
        Password = "123456",
        isActive = true
      },new Account()
      {
        Email = "Q@P.com",
        Password = "123456",
        isActive = true
      }};
      return accounts;
    }

    public static List<Account> MockAccountEmptyData()
    {
      return new List<Account>();
    }
  }
}
