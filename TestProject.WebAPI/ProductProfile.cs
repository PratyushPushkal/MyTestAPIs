using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.WebAPI.Dtos;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI
{
  public class ProductProfile : Profile
  {
    public ProductProfile()
    {
      CreateMap<User, UserDto>();
      CreateMap<UserDto, User>();

      CreateMap<Account, AccountDto>();
      CreateMap<AccountDto, Account>();

      CreateMap<Account, AccountUpdateDto>();
      CreateMap<AccountUpdateDto, Account>();

      CreateMap<Account, AccountCreateDto>();
      CreateMap<AccountCreateDto, Account>();
    }
  }
}
