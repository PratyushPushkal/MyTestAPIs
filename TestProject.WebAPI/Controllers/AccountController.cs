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
  public class AccountController : ControllerBase
  {
    private IAccountData _accountdata;
    private readonly IMapper _mapper;
    public AccountController(IAccountData userdata, IMapper mapper)
    {
      _accountdata = userdata;
      _mapper = mapper;
    }

    #region Read
    [HttpGet]
    public async Task<IActionResult> accounts()
    {
      try
      {
        var result = await _accountdata.ListAccount();
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

    [HttpGet]
    [Route("{accId}")]
    public async Task<IActionResult> accounts(int accId)
    {
      try
      {
        var result = await _accountdata.GetAccountById(accId);
        return Ok(result);
      }
      catch
      {
        throw;
      }
    }

    [HttpPost]
    public async Task<bool> Login([FromBody]AccountDto _account)
    {
      try
      {
        var result = await _accountdata.Login(_mapper.Map<Account>(_account));
        return result;
      }
      catch
      {
        throw;
      }
    }
    #endregion

    #region CUD
    [HttpPost]
    public async Task<IActionResult> accounts([FromBody] AccountUpdateDto _account)
    {
      try
      {
        var result = await _accountdata.CreateAccount(_mapper.Map<Account>(_account));
        return Ok(result);
      }
      catch
      {
        throw;
      }
    }

    [HttpPut]
    public async Task<IActionResult> accounts([FromBody] AccountDto _account)
    {
      try
      {
        var result = await _accountdata.UpdatePassword(_mapper.Map<Account>(_account));
        return Ok(result);
      }
      catch
      {
        throw;
      }
    }

    [HttpDelete]
    public async Task<IActionResult> accounts([FromBody]Account account)
    {
      try
      {
        var result = await _accountdata.DeleteAccountById(account.AccountId);
        return Ok(result);
      }
      catch
      {
        throw;
      }
    }
    #endregion
  }
}
