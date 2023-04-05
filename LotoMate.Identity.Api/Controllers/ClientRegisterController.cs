using Identity.API.Application.Command;
using LotoMate.Framework;
using LotoMate.Framework.Authorisation;
using LotoMate.Identity.Api.Handlers;
using LotoMate.Identity.API.Handlers;
using LotoMate.Identity.API.ViewModels;
using LotoMate.Identity.Infrastructure;
using LotoMate.Identity.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LotoMate.Identity.API.Controllers
{
    public class ClientRegisterController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly ILogger<ClientRegisterController> logger;
        private readonly IMediator mediator;
        private readonly IUnitOfWork<IdentityContext> unitOfWork;

        public ClientRegisterController(
           UserManager<User> userManager,
           RoleManager<Role> roleManager,
           ILogger<ClientRegisterController> logger,
           IMediator mediator,
           IUnitOfWork<IdentityContext> unitOfWork
            , IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("registerClient")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterClient([FromBody] RBAClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Add Client
                    var clientRequest = new AddNewClientRequest() { Client = model, UserId = UserId };
                    var res1 = await mediator.Send(clientRequest);

                    //Register User
                    var addUserRequest = new AddUserRequest()
                    {
                        User = new UserViewModel()
                        {
                            UserName = model.UserName,
                            FirstName = model.ClientFname,
                            LastName = model.ClientLname,
                            Email = model.EmailId,
                            PhoneNumber = model.Phone,
                            Password = model.Password,
                            ConfirmPassword = model.ConfirmPassword
                        }
                    };
                    var res2 = await mediator.Send(addUserRequest);

                    //Find Admin Role
                    var adminRole = await roleManager.FindByNameAsync("ClientAdmin");

                    //Add Admin role for User
                    var role = new UserClientRoleViewModel() { ClientId = res1.ClientId, UserId = res2.UserId, RoleId = adminRole.Id, IsActive = false };
                    var roleRequest = new AddClientAdminRoleRequest() { Role = role, UserId = UserId };
                    var res3 = await mediator.Send(roleRequest);
                    logger.LogInformation("Client User is assigned Admin role User : {Id}", res2.UserId);
                    //save changes
                    await unitOfWork.SaveChangesAsync();

                    var message = res1.Exists && res2.Exists && res3.Exists ? "Client is already registered!"
                                    : "Client Registration successful.";
                    return Ok(new MessageViewModel
                    {
                        StatusCode = 200,
                        Message = message
                    });
                }
                catch (Exception e)
                {
                    return HandleException(e, "Registration");
                }
            }
            return BadRequest("UNKNOWN_ERROR");
        }

        [HttpGet("getStores/{clientId}")]
        [Authorize(Policy = AuthPolicy.ClientAdmin)]
        public async Task<IActionResult> GetStores(int clientId)
        {
            try
            {
                var stores = await mediator.Send(new GetStoreRequest() { ClientId= clientId, UserId = UserId  });
                return Ok(stores.Stores);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching Stores Detail!");
                return HandleException(ex, "GetStores");
            }
        }


    }
}
