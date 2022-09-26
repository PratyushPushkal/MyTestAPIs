using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace TestProject.WebAPI
{
  public static class Common
  {
    public static string GetHashedPassword(string password)
    {
      var salt = new byte[100];
      string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
          password: password!,
          salt: salt,
          prf: KeyDerivationPrf.HMACSHA256,
          iterationCount: 100000,
          numBytesRequested: 256 / 8));

      return hashedPassword;
    }
  }
}
