using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TestProject.WebAPI.Middlewares
{
  public class HandlingErrorMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<HandlingErrorMiddleware> _logger;

    public HandlingErrorMiddleware(RequestDelegate next, ILogger<HandlingErrorMiddleware> logger)
    {
      _next = next;
      _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch(Exception ex)
      {
        await HandleExceptionAsync(context, ex , _logger);
      }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<HandlingErrorMiddleware> logger)
    {
      object errors = null;
      switch(ex)
      {
        case RestException re:
          logger.LogDebug(ex, "Rest Error");
          errors = re.Error;
          context.Response.StatusCode = (int)re.StatusCode;
          break;
        case Exception e:
          logger.LogError(ex, "Server Error");
          errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
          context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
          break;
      }
      if (errors != null)
      {
        context.Response.ContentType = "application/json";
        var errorLits = new
        {
          message = "List of Errors",
          errorList = errors
        };
        var result = JsonConvert.SerializeObject(errorLits);
        await context.Response.WriteAsync(result);
      }
    }
  }
}
