using LotoMate.Client.Api.Handlers;
using LotoMate.Client.Api.ViewModels;
using LotoMate.Client.Infrastructure;
using LotoMate.Framework;
using LotoMate.Framework.Authorisation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LotoMate.Client.Api.Controllers
{
    [Authorize(Policy = AuthPolicy.UserAccess)]
    public class ClientController : BaseController
    {
        private readonly ILogger<ClientController> logger;
        private readonly IMediator mediator;
        private readonly IUnitOfWork<LottoClientDbContext> unitOfWork;

        public ClientController(ILogger<ClientController> logger, IMediator mediator, IUnitOfWork<LottoClientDbContext> unitOfWork) : base(logger)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RBAClientViewModel clientView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                
                var client = await mediator.Send(new AddNewClientRequest() { Client = clientView});
                await unitOfWork.SaveChangesAsync();
                return Ok(client);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while adding Client Detail.");
                return HandleException(ex, "Insert");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching Client Detail Id {Id}", id);
                return HandleException(ex, "Get");
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RBAClientViewModel clientView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while updating Client Detail.");
                return HandleException(ex, "Update");
            }
        }
    }
}
