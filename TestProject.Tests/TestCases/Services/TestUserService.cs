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
    public async Task ListUsers_ReturnCollection()
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
    public void Dispose()
    {
      _dbContext.Database.EnsureDeleted();
      _dbContext.Dispose();
    }
  }
}
