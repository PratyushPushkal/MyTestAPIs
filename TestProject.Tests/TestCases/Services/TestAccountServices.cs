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
    public async Task ListAccount_ReturnCollection()
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
    public void Dispose()
    {
      _dbContext.Database.EnsureDeleted();
      _dbContext.Dispose();
    }
  }
}
