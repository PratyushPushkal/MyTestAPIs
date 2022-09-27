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
  public class TestUserService : IDisposable
  {
    private readonly ApplicationContext _dbContext;
    public TestUserService()
    {
      var option = new DbContextOptionsBuilder<ApplicationContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _dbContext = new ApplicationContext(option);
        _dbContext.Database.EnsureCreated();
    }
    [Fact]
    public async Task Users_ReturnCollection()
    {
      ///Arrange
      _dbContext.Users.AddRange(MockData.MockUserData());
      _dbContext.SaveChanges();
      var sut = new UserData(_dbContext);
      ///ACT
      var result = await sut.ListUsers();
      ///Assert
      result.Should().HaveCount((MockData.MockUserData().Count));
    }

    [Fact]
    public async Task Users_Create()
    {
      ///Arrange
      User user = MockData.GetUser();
      var sut = new UserData(_dbContext);
      ///ACT
      var result = await sut.CreateUser(user);
      ///Assert
      result.Should().Equals(true);
    }

    [Fact]
    public async Task Users_CreateForExistingEmail()
    {
      ///Arrange
      User user = MockData.GetUser();
      _dbContext.Users.AddRange(MockData.MockUserData());
      _dbContext.SaveChanges();
      var sut = new UserData(_dbContext);
      ///ACT
      string message = string.Empty;
      try
      {
        var result = await sut.CreateUser(user);
      }
      catch(Exception ex)
      {
        message = ex.Message;
      }
      ///Assert
      message.Should().Equals("Email is already used with different user.");
    }

    [Fact]
    public async Task Users_CreateSalaryExpensesDiffLessThan1000()
    {
      ///Arrange
      User user = new User
      {
        Email = "P@P.com",
        Expenses = 15000,
        Salary = 20000,
        Name = "Pratyush"
      };
      _dbContext.Users.AddRange(MockData.MockUserData());
      _dbContext.SaveChanges();
      var sut = new UserData(_dbContext);
      ///ACT
      string message = string.Empty;
      try
      {
        var result = await sut.CreateUser(user);
      }
      catch (Exception ex)
      {
        message = ex.Message;
      }
      ///Assert
      message.Should().Equals("`monthly salary - monthly expenses` is less than $1000, Can't create user.");
    }

    [Fact]
    public async Task Users_ReturnsSingleUserFound()
    {
      ///Arrange
      _dbContext.Users.AddRange(MockData.MockUserData());
      _dbContext.SaveChanges();
      var sut = new UserData(_dbContext);
      ///ACT
      var id = 1;
      var result = await sut.GetUser(id);
      ///Assert
      result.UserId.Should().Equals(id);
    }
    [Fact]
    public async Task Users_ReturnsSingleUserNotFound()
    {
      ///Arrange
      _dbContext.Users.AddRange(MockData.MockUserData());
      _dbContext.SaveChanges();
      var sut = new UserData(_dbContext);
      ///ACT
      var id = 4;
      string message = string.Empty;
      try
      {
        var res = await sut.GetUser(id);
      }
      catch (Exception ex)
      {
        message = ex.Message;
      }
      ///Assert
      message.Should().Equals("User not found.");
    }

    public void Dispose()
    {
      _dbContext.Database.EnsureDeleted();
      _dbContext.Dispose();
    }
  }
}
