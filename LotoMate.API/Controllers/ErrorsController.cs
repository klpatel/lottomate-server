using LotoMate.Exceptions;
using LotoMate.Identity.API.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;

namespace LotoMate.API.Controllers
{
    [Route("/errors")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        private readonly ILogger<ErrorsController> logger;
        public ErrorsController(ILogger<ErrorsController> logger)
        {
            this.logger = logger;
        }

        #region Targeted statndard Error codes
        ////Status code used and expose so far
        ////////////Handled at application top layer////////////////  
        //404 Not Found  
        //500 Internal Server Error
        //501 Not Implemented
        //503 Service Unavailable
        ////////////Handled by Common Identity module 
        //401 Unauthorized : Authentication failure - Invalid credentials, invalid/expired token 
        //403 Forbidden : Authorisation failure 
        ////////////Handled from code at mainly in handlers sometimes at controller level 
        ///200 Success : Default for all successful requests
        //400 Bad Request : For any request cannot be served by API controller
        //422 For all input/validation errors/ business validations / any Custom application exceptions 
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error(int? statusCode = null)
        {
            var originalPath = HttpContext.Features.Get<IStatusCodeReExecuteFeature>()?.OriginalPath;

            if (statusCode.HasValue)
            {
                string reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode.Value);
                logger.LogError(reasonPhrase);
                return StatusCode(statusCode.Value, new
                {
                    error = new ErrorViewModel(statusCode.Value, originalPath)
                });
            }

            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>() as ExceptionHandlerFeature;
            var exception = feature?.Error;
            if (exception != null)
            {
                logger.LogError(exception, "Exception is handled from ErrorController.");
                return ResolveCustomException(exception);
            }

            return StatusCode(500, new
            {
                error = new ErrorViewModel(500, originalPath, null, new string[] { "Unknown Error" })
            });
        }
        private ObjectResult ResolveCustomException(Exception ex)
        {         
            var message = "Error while performing requested action. Please start over by refreshing page if you encounter the issue again.";

            if (ex != null)
            {
                if (ex is InvalidCredentialException)
                    return StatusCode(401, ex.Message);
                else if (ex is InvalidTokenException)
                    return StatusCode(401, ex.Message);
                else if (ex is ChangePasswordException)
                    return StatusCode(422, ex.Message);
                else if (ex is ResetPasswordException)
                    return StatusCode(422, ex.Message);
                else if (ex is AuthorisationFailException)
                    return StatusCode(403, ex.Message);
            }
            return StatusCode(500, message);
        }
        

    }
}
