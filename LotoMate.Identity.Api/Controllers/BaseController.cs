using LotoMate.Exceptions;
using LotoMate.Framework.Authorisation;
using LotoMate.Identity.API.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Security.Claims;

namespace LotoMate.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthPolicy.UserAccess)]
    [ApiExplorerSettings(GroupName = "LotoMate.Identity.API")]
    public class BaseController : ControllerBase
    {
        private readonly ILogger logger;

        public int? UserId { get; set; }
        public string UserName { get; set; }

        public BaseController(ILogger logger)
        {
            this.logger = logger;

            this.UserId = null;
            if (User != null)
                this.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
        public BaseController(ILogger logger, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            var user = httpContextAccessor.HttpContext.User;
            this.UserId = null;
            if (user != null)
            {
                int uid;
                int.TryParse(user.FindFirstValue("userid"), out uid);
                this.UserId = uid;
                this.UserName = user.FindFirstValue("unique_name");
            }
        }

        protected ObjectResult StatusCodeActionResult(string message, int statusCode=422)
        {
            logger.LogError(message);
            string output = JsonConvert.SerializeObject(new MessageResponse(statusCode, message));
            return new ObjectResult(output) { StatusCode = statusCode };
        }

        protected ObjectResult HandleException(Exception exception, string action)
        {
            var message = exception.Message;
            logger.LogError(exception, message);

            string output = JsonConvert.SerializeObject(new MessageResponse(422, message));
            if (!(exception is IdentityException || exception is DuplicateEmailException 
                    || exception is DuplicateUserNameException || exception is RecordNotFoundException))
                message = "Error while performing " + action + ". Please start over by refreshing page if you encounter the issue again.";
            return new ObjectResult(output) { StatusCode = 422 };
        }
    }
}
