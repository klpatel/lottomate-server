using LotoMate.Framework;
using LotoMate.Framework.Authorisation;
using LotoMate.Lottery.Api.Handlers.IStore;
using LotoMate.Lottery.Infrastructure;
using LotoMate.Lottery.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Api.Controllers
{
    [Authorize(Policy = AuthPolicy.ClientUser)]
    //[AllowAnonymous]
    public class StoreController : BaseController
    {
        private readonly ILogger<StoreController> logger;
        private readonly IMediator mediator;
        private readonly IUnitOfWork<LotteryDbContext> unitOfWork;

        public StoreController(ILogger<StoreController> logger, IMediator mediator, 
            IUnitOfWork<LotteryDbContext> unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger,httpContextAccessor)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Stores(int id)
        //{
        //    try
        //    {
        //        var game = await mediator.Send(new GetGameRequest() { Id = id });
        //        return Ok(game.Game);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex, "Error while fetching Game Detail Id {Id}", id);
        //        return HandleException(ex, "Get");
        //    }
        //}
        //[HttpGet]
        //public async Task<IActionResult> Stores()
        //{
        //    try
        //    {
        //        var games = await mediator.Send(new GetAllGameRequest() {});
        //        return Ok(games.Games);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex, "Error while fetching Games Detail Id");
        //        return HandleException(ex, "GetAll");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StoreVM store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addstore = await mediator.Send(new AddNewStoreRequest() { Store = store , UserId = UserId });
                return Ok(addstore);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while adding Store  Detail.");
                return HandleException(ex, "Insert");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] StoreVM store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addstore = await mediator.Send(new UpdateStoreRequest() { Store = store, UserId = UserId });
                await unitOfWork.SaveChangesAsync();
                return Ok(addstore);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while updating store  Detail.");
                return HandleException(ex, "Update");
            }
        }

    }
}
