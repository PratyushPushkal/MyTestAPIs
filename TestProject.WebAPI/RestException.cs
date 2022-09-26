using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TestProject.WebAPI
{
  public class RestException : Exception
  {
    public HttpStatusCode StatusCode { get; }
    public object Error { get; }
    public RestException(HttpStatusCode Code, object error)
    {
      StatusCode = Code;
      Error = error;
    }
  }
}
