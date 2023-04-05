using Identity.API.Application.Command;
using LotoMate.Exceptions;
using LotoMate.Framework;
using LotoMate.Framework.Authorisation;
using LotoMate.Identity.API.Extensions;
using LotoMate.Identity.API.Handlers;
using LotoMate.Identity.API.ViewModels;
using LotoMate.Identity.Infrastructure;
using LotoMate.Identity.Infrastructure.Models;
using LotoMate.Identity.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LotoMate.Identity.API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly IConfiguration config;
        private readonly ILogger<AccountController> logger;
        private readonly IMediator mediator;
        private readonly ITokenGeneratorService tokenGeneratorService;
        private readonly IUnitOfWork<IdentityContext> unitOfWork;
        private readonly IConfiguration Configuration;

        public AccountController(
           UserManager<User> userManager,
           RoleManager<Role> roleManager,
           ITokenGeneratorService tokenGeneratorService,
           IConfiguration config,
           ILogger<AccountController> logger,
           IMediator mediator,
           IUnitOfWork<IdentityContext> unitOfWork,
           IConfiguration configuration

            ) : base(logger)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.config = config;
            this.logger = logger;
            this.mediator = mediator;
            this.tokenGeneratorService = tokenGeneratorService;
            this.unitOfWork = unitOfWork;
            this.Configuration = configuration;
        }

        /// <summary>
        /// Enables User Login
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.Users
                    .FirstOrDefaultAsync(u => u.Email == model.UserName || u.UserName == model.UserName);

                    //check for imported user password
                    if (user != null && string.IsNullOrEmpty(user.PasswordHash))
                    {
                        var tempPass = IdentityPolicy.GenerateRandomPassword();
                        user.PasswordHash = userManager.PasswordHasher.HashPassword(user, tempPass);
                        user.SecurityStamp = Guid.NewGuid().ToString("D");
                        await userManager.UpdateAsync(user);
                        //emailSender.TempPasswordAsycForImported(user.Email, tempPass, user.FirstName);
                        return StatusCodeActionResult("New Temporary password is sent to your email. Please use those credentials to login to the system.", 412);
                    }

                    if (user != null)
                    {
                        var signedIn = await userManager.CheckPasswordAsync(user, model.Password);

                        if (!signedIn)
                        {
                            return StatusCodeActionResult("Username and/or password is incorrect!", 412);
                        }
                        var roles = await userManager.GetRolesAsync(user);
                        var tokendata = await tokenGeneratorService.GenerateTokens(user, roles);

                        return Ok(tokendata);
                    }

                    return StatusCodeActionResult("Username and/or password is incorrect!", 412);
                }
                catch (Exception ex)
                {
                    return HandleException(ex, "Login");
                }
            }
            return BadRequest();
        }

        [HttpPost("register")]
        [Authorize(Policy = AuthPolicy.ClientAdmin)]
        //[AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //check if ClientId and StoreId are correct
                    var checkClientStore = new GetClientStoreRequest() { ClientId = model.ClientId, StoreId = model.StoreId, UserId = UserId };
                    var verify = await mediator.Send(checkClientStore);
                    if (!verify.Exists)
                        throw new InvalidParameterException("Invalid ClientId/StoreId");

                    var registrationCommand = new RegistrationCommand() { Users = model };
                    var result = await mediator.Send(registrationCommand);

                    //Find User Role
                    var userRole = await roleManager.FindByNameAsync("ClientUser");

                    //Add Admin role for User
                    var role = new UserClientRoleViewModel() { ClientId = model.ClientId, StoreId=model.StoreId, UserId = result.Id, RoleId = userRole.Id, IsActive = false };
                    var roleRequest = new AddClientAdminRoleRequest() { Role = role, UserId = UserId };
                    var res2 = await mediator.Send(roleRequest);
                    await unitOfWork.SaveChangesAsync();

                    return Ok(new MessageViewModel
                    {
                        StatusCode = 200,
                        Message = "Registration successful."
                    });
                }
                catch (Exception e)
                {
                    return HandleException(e, "Registration");
                }
            }
            return BadRequest("UNKNOWN_ERROR");
        }

        [HttpPost("refreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] Token token)
        {
            var principal = tokenGeneratorService.GetPrincipalFromExpiredToken(token.AccessToken);

            if (principal != null)
            {
                string userName = principal.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.UniqueName)?.Value;
                var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
                var savedRefreshToken = await userManager.VerifyUserTokenAsync(user, "LotoMate", "RefreshToken", token.RefreshToken);

                if (!savedRefreshToken)
                {
                    return StatusCodeActionResult("Invalid refresh token!", 401);
                }
                var roles = await userManager.GetRolesAsync(user);
                return Ok(await tokenGeneratorService.GenerateTokens(user, roles));
            }
            return BadRequest();
        }

        [HttpPost("logout")]
        [Authorize(Policy = AuthPolicy.UserAccess)]
        public async Task<IActionResult> Logout()
        {
            string userName = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.UniqueName)?.Value;
            if (!String.IsNullOrEmpty(userName))
            {
                var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
                var updateSecurityStamp = await userManager.UpdateSecurityStampAsync(user);
                if (updateSecurityStamp.Succeeded)
                {
                    return Ok(new MessageViewModel { StatusCode = 200, Message = "Logged out Successfully" });
                }
            }
            return BadRequest();
        }

        [HttpGet("forgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            string message = "We have received your request. You will receive an email shortly if we found you in our system.";
            //var user = await userManager.FindByEmailAsync(email);
            var user = await userManager.Users
                    .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                logger.LogError($"The email {email} is not registered in the system");
                return Ok(new MessageViewModel { StatusCode = 200, Message = message });
            }
            
            //reset password
            var tempPass = IdentityPolicy.GenerateRandomPassword();
            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, tempPass);
            await userManager.UpdateAsync(user);

            // Send an email with this link
            //var loginUrl = config["Identity:Login"];
            // emailSender.TempPasswordAsyc(email, tempPass, loginUrl);
            return Ok(new MessageViewModel { StatusCode = 200, Message = message });
        }

        [HttpPost("resetPassword")]
        //[AllowAnonymous]
        [Authorize(Policy = AuthPolicy.UserAccess)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassViewModel model)
        {
            string errorMessage = "Error while resetting the user password.";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByIdAsync(model.Id.ToString());
            if (user == null)
            {
                errorMessage = $"User {model.Id} is not present in the database.";
                logger.LogError(errorMessage);
                return StatusCodeActionResult(errorMessage);

            }

            var result = await userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(model.ConfirmationCode), model.Password);
            if (!result.Succeeded)
            {
                errorMessage = $"Error while resetting password for the User {model.Id}. {JsonConvert.SerializeObject(result.Errors)}";
                logger.LogError(errorMessage);
                return StatusCodeActionResult("Password Mismatch");
            }
            return Ok(new MessageViewModel { StatusCode = 200, Message = "Successfully reset the user password" });
        }

        [HttpPost("changePassword")]
        //[AllowAnonymous]
        [Authorize(Policy = AuthPolicy.UserAccess)]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            //var user = await userManager.FindByNameAsync(model.UserName);
            var user = await userManager.Users
                   .FirstOrDefaultAsync(u => u.Email == model.UserName || u.UserName == model.UserName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
                return StatusCodeActionResult(JsonConvert.SerializeObject(result.Errors));
            return Ok(new MessageViewModel { StatusCode = 200, Message = "Password changed successfully." });
        }

        [HttpPost("requestToken")]
        //[AllowAnonymous]
        [Authorize(Policy = AuthPolicy.UserAccess)]
        public async Task<ActionResult<AccessTokensResponse>> RequestToken([FromForm] LoginRequest request)
        {
            // this version of login is called by swagger ui
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            var signedIn = await userManager.CheckPasswordAsync(user, request.Password);

            if (!signedIn)
            {
                return StatusCodeActionResult("Username and/or password is incorrect!", 401);
            }
            var roles = await userManager.GetRolesAsync(user);
            var token = await tokenGeneratorService.GenerateTokens(user, roles);
            return Ok(new AccessTokensResponse(token));
        }
       
    }
}

