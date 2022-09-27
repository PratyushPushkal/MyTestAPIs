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

namespace TestProject.Tests.TestCases
{
  public class TestUserController
  {
    [Fact]
    public async Task ListUsers_ShouldReturn200StatusCode()
    {
      //Arrange
      var userData = new Mock<IUserData>();
      userData.Setup(x => x.ListUsers()).ReturnsAsync(MockData.MockUserData());
      var mockMapper = new Mock<IMapper>();
      var sut = new UserController(userData.Object, mockMapper.Object);
      //Act
      var result = await sut.users();
      //Assert
      (result as OkObjectResult).StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task ListUsers_ShouldReturnNoContentStatusCode()
    {
      //Arrange
      var userData = new Mock<IUserData>();
      userData.Setup(x => x.ListUsers()).ReturnsAsync(MockData.MockUserEmptyData());
      var mockMapper = new Mock<IMapper>();
      var sut = new UserController(userData.Object, mockMapper.Object);
      //Act
      var result = await sut.users();
      //Assert
      (result as NoContentResult).StatusCode.Should().Be(204);
    }
  }
}
