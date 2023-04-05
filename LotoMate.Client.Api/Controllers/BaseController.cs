using LotoMate.Client.Api.ViewModels;
using LotoMate.Exceptions;
using LotoMate.Framework.Authorisation;
using LotoMate.Framework.EnumModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Security.Claims;

namespace LotoMate.Client.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthPolicy.UserAccess)]
    [ApiExplorerSettings(GroupName = "LotoMate.Client.API")]
    public class BaseController : ControllerBase
    {
        private readonly ILogger logger;

        public int? UserId { get; set; }

        public BaseController(ILogger logger)
        {
            this.logger = logger;

            this.UserId = null;
            if (User != null)
                this.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        protected ObjectResult StatusCodeActionResult(string message, int statusCode = 422)
        {
            logger.LogError(message);

            string output = JsonConvert.SerializeObject(new ErrorResponse(statusCode, message));
            return new ObjectResult(output) { StatusCode = statusCode };
        }

        protected ObjectResult HandleException(Exception exception, string action)
        {
            var message = exception.Message;
            logger.LogError(exception, message);

            if (!(exception is RecordNotFoundException || exception is DuplicateNameException 
                || exception is InvalidParameterException || exception is RecordFoundException))
                message = "Error while performing requested action " + action + ". Please start over by refreshing page if you encounter the issue again.";

            string output = JsonConvert.SerializeObject(new ErrorResponse(422, message));
            return new ObjectResult(output) { StatusCode = 422 };
        }
    }
}
