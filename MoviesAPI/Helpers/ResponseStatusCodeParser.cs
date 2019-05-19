using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using Newtonsoft.Json;

namespace MoviesAPI.Common
{
    public static class ResponseStatusCodeParser
    {
        public static IActionResult Parse(IEnumerable<IError> errors, object result)
        {

            if (errors == null || !errors.Any())
            {
                return new OkObjectResult(result);
            }

            Func<IError, bool> server = error => error.Code.StartsWith("ServerError");
            Func<IError, bool> notFound = error => error.Code.StartsWith("NotFound");
            Func<IError, bool> forbidden = error => error.Code.StartsWith("Forbidden_");
            Func<IError, bool> unAuthorized = error => error.Code.StartsWith("UnAuthorized");
            Func<IError, bool> badRequest = error => error.Code.StartsWith("bad");
            Func<IError, bool> conflict = error => error.Code.StartsWith("Conflict");

            if (errors.Any(server))
            {
                return new ContentResult() { Content = JsonConvert.SerializeObject(result), StatusCode = StatusCodes.Status500InternalServerError };
            }
            if (errors.Any(notFound))
            {
                return new ContentResult() { Content= JsonConvert.SerializeObject(result), StatusCode= StatusCodes.Status404NotFound };
            }
            if (errors.Any(forbidden))
            {
                return new ContentResult() { Content = JsonConvert.SerializeObject(result), StatusCode = StatusCodes.Status403Forbidden };
            }
            if (errors.Any(unAuthorized))
            {
                return new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
            if (errors.Any(badRequest))
            {
                return new ContentResult() { Content = JsonConvert.SerializeObject(result), StatusCode = StatusCodes.Status400BadRequest };
            }
            if (errors.Any(conflict))
            {
                return new ContentResult() { Content = JsonConvert.SerializeObject(result), StatusCode = StatusCodes.Status409Conflict };
            }

            return new BadRequestObjectResult(result);

        }
    }
}
