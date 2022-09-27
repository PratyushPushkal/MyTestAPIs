using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.WebAPI.Data;
using TestProject.WebAPI.Dtos;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private IUserData _userdata;
    private readonly IMapper _mapper;
    public UserController(IUserData userdata, IMapper mapper)
    {
      _userdata = userdata;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> users()
    {
      try
      {
        var result = await _userdata.ListUsers();
        if (result.Count == 0)
        {
          return NoContent();
        }
        return Ok(result);
      }
      catch
      {
        throw;
      }
    }

    [HttpPost]
    public async Task<IActionResult> users([FromBody] UserDto _user)
    {
      try
      {
        var result = await _userdata.CreateUser(_mapper.Map<User>(_user));
        if (result)
          return Ok(result);
        else
          return NoContent();
      }
      catch
      {
        throw;
      }
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> users(int userId)
    {
      try
      {
        var result = await _userdata.GetUser(userId);
        return Ok(result);
      }
      catch
      {
        throw;
      }
    }
  }
}
