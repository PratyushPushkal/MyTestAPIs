using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.WebAPI.Controllers;
using TestProject.WebAPI.Data;
using Xunit;

namespace TestProject.Tests.TestCases.Controller
{
  public class TestAccountController
  {
    [Fact]
    public async Task ListAccount_ShouldReturn200StatusCode()
    {
      //Arrange
      var data = new Mock<IAccountData>();
      data.Setup(x => x.ListAccount()).ReturnsAsync(MockData.MockAccountData());
      var mockMapper = new Mock<IMapper>();
      var sut = new AccountController(data.Object, mockMapper.Object);
      //Act
      var result = await sut.accounts();
      //Assert
      (result as OkObjectResult).StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task ListAccount_ShouldReturnNoContentStatusCode()
    {
      //Arrange
      var data = new Mock<IAccountData>();
      data.Setup(x => x.ListAccount()).ReturnsAsync(MockData.MockAccountEmptyData());
      var mockMapper = new Mock<IMapper>();
      var sut = new AccountController(data.Object, mockMapper.Object);
      //Act
      var result = await sut.accounts();
      //Assert
      (result as NoContentResult).StatusCode.Should().Be(204);
    }
  }
}
