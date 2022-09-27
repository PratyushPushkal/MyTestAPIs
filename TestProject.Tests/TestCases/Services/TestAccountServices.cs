using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.WebAPI.Data;
using TestProject.WebAPI.Models;
using Xunit;

namespace TestProject.Tests.TestCases.Services
{
  public class TestAccountServices : IDisposable
  {
    private readonly ApplicationContext _dbContext;
    public TestAccountServices()
    {
      var option = new DbContextOptionsBuilder<ApplicationContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
      _dbContext = new ApplicationContext(option);
      _dbContext.Database.EnsureCreated();
    }
    [Fact]
    public async Task Accounts_ReturnCollection()
    {
      ///Arrange
      _dbContext.Accounts.AddRange(MockData.MockAccountData());
      _dbContext.SaveChanges();
      var sut = new AccountData(_dbContext);
      ///ACT
      var result = await sut.ListAccount();
      ///Assert
      result.Should().HaveCount((MockData.MockUserData().Count));
    }

    [Fact]
    public async Task Accounts_Create()
    {
      ///Arrange
      _dbContext.Users.AddRange(MockData.MockUserData());
      _dbContext.SaveChanges();
      Account account = MockData.GetAccount();
      var sut = new AccountData(_dbContext);
      ///ACT
      var result = await sut.CreateAccount(account);
      ///Assert
      result.Should().Equals(true);
    }

    [Fact]
    public async Task Accounts_CreateUserNotAvailableForGivenEmail()
    {
      ///Arrange
      Account account = MockData.GetAccount();
      var sut = new AccountData(_dbContext);
      ///ACT
      string message = string.Empty;
      try
      {
        var result = await sut.CreateAccount(account);
      }
      catch (Exception ex)
      {
        message = ex.Message;
      }
      ///Assert
      message.Should().Equals("User not available for given Email");
    }

    [Fact]
    public async Task Accounts_CreateEmailAlreadyExistWithOtherUser()
    {
      ///Arrange
      Account account = new Account
      {
        AccountId = 5,
        Email = "Q@P.com",
        Password = "123456",
        isActive = true
      };
      _dbContext.Accounts.AddRange(MockData.MockAccountData());
      _dbContext.SaveChanges();
      var sut = new AccountData(_dbContext);
      ///ACT
      string message = string.Empty;
      try
      {
        var result = await sut.CreateAccount(account);
      }
      catch (Exception ex)
      {
        message = ex.Message;
      }
      ///Assert
      message.Should().Equals("Email is already used with different Account.");
    }

    [Fact]
    public async Task Accounts_DeleteFound()
    {
      ///Arrange
      _dbContext.Accounts.AddRange(MockData.MockAccountData());
      _dbContext.SaveChanges();
      var sut = new AccountData(_dbContext);
      ///ACT
      var accid = 1;
      var result = await sut.DeleteAccountById(accid);
      ///Assert
      result.Should().Equals(true);
    }

    [Fact]
    public async Task Accounts_DeleteNotFound()
    {
      ///Arrange
      Account account = new Account
      {
        AccountId = 5,
        Email = "Q@P.com",
        Password = "123456",
        isActive = true
      };
      _dbContext.Accounts.AddRange(MockData.MockAccountData());
      _dbContext.SaveChanges();
      var sut = new AccountData(_dbContext);
      ///ACT
      string message = string.Empty;
      try
      {
        var result = await sut.DeleteAccountById(account.AccountId);
      }
      catch (Exception ex)
      {
        message = ex.Message;
      }
      ///Assert
      message.Should().Equals("Account not found.");
    }

    [Fact]
    public async Task Accounts_ReturnSingleAccountFound()
    {
      ///Arrange
      _dbContext.Accounts.AddRange(MockData.MockAccountData());
      _dbContext.SaveChanges();
      var sut = new AccountData(_dbContext);
      ///ACT
      var accid = 1; 
      var result = await sut.GetAccountById(accid);
      ///Assert
      result.AccountId.Should().Equals(accid);
    }

    [Fact]
    public async Task Accounts_ReturnSingleAccountNotFound()
    {
      ///Arrange
      _dbContext.Accounts.AddRange(MockData.MockAccountData());
      _dbContext.SaveChanges();
      var sut = new AccountData(_dbContext);
      ///ACT
      var accid = 4;
      string message = string.Empty;
      try
      {
        var result = await sut.GetAccountById(accid);
      }
      catch (Exception ex)
      {
        message = ex.Message;
      }
      ///Assert
      message.Should().Equals("Account not found.");
    }

    [Fact]
    public async Task Accounts_UpdatePasswordFound()
    {
      ///Arrange
      Account account = new Account {
        Email = "P@P.com",
        Password = "654321",
      };
      _dbContext.Accounts.AddRange(MockData.MockAccountData());
      _dbContext.SaveChanges();
      var sut = new AccountData(_dbContext);
      ///ACT
      var result = await sut.UpdatePassword(account);
      ///Assert
      result.Should().Equals(true);
    }

    [Fact]
    public async Task Accounts_UpdatePasswordNotFound()
    {
      ///Arrange
      Account account = new Account
      {
        Email = "Qwqeqw@P.com",
        Password = "123456"
      };
      _dbContext.Accounts.AddRange(MockData.MockAccountData());
      _dbContext.SaveChanges();
      var sut = new AccountData(_dbContext);
      ///ACT
      string message = string.Empty;
      try
      {
        var result = await sut.UpdatePassword(account);
      }
      catch (Exception ex)
      {
        message = ex.Message;
      }
      ///Assert
      message.Should().Equals("No Account with given Email present to update.");
    }

    [Fact]
    public async Task Accounts_LoginSuccessful()
    {
      ///Arrange
      Account account = new Account
      {
        Email = "P@P.com",
        Password = "123456",
      };
      _dbContext.Accounts.AddRange(MockData.MockAccountData());
      _dbContext.SaveChanges();
      var sut = new AccountData(_dbContext);
      ///ACT
      var result = await sut.Login(account);
      ///Assert
      result.Should().Equals(true);
    }

    [Fact]
    public async Task Accounts_LoginUnSuccessful()
    {
      ///Arrange
      Account account = new Account
      {
        Email = "P@P.com",
        Password = "6541213"
      };
      _dbContext.Accounts.AddRange(MockData.MockAccountData());
      _dbContext.SaveChanges();
      var sut = new AccountData(_dbContext);
      ///ACT
      var result = await sut.Login(account);
      ///Assert
      result.Should().Equals(true);
    }

    [Fact]
    public async Task Accounts_LoginWrongEmail()
    {
      ///Arrange
      Account account = new Account
      {
        Email = "PQWQW@P.com",
        Password = "6541213"
      };
      _dbContext.Accounts.AddRange(MockData.MockAccountData());
      _dbContext.SaveChanges();
      var sut = new AccountData(_dbContext);
      ///ACT
      string message = string.Empty;
      try
      {
        var result = await sut.Login(account);
      }
      catch (Exception ex)
      {
        message = ex.Message;
      }
      ///Assert
      message.Should().Equals("Account not found.");
    }
    public void Dispose()
    {
      _dbContext.Database.EnsureDeleted();
      _dbContext.Dispose();
    }
  }
}
