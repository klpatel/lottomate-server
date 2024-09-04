using LotoMate.Exceptions;
using LotoMate.Framework;
using LotoMate.Framework.Authorisation;
using LotoMate.Lottery.Api.Handlers.Game;
using LotoMate.Lottery.Api.Handlers.GameMaster;
using LotoMate.Lottery.Infrastructure;
using LotoMate.Lottery.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Api.Controllers
{
    [Authorize]
    //[AllowAnonymous]
    public class GamesController : BaseController
    {
        private readonly ILogger<GamesController> logger;
        private readonly IMediator mediator;
        private readonly IUnitOfWork<LotteryDbContext> unitOfWork;

        public GamesController(ILogger<GamesController> logger, IMediator mediator, 
            IUnitOfWork<LotteryDbContext> unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger,httpContextAccessor)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Games(int id)
        {
            try
            {
                var game = await mediator.Send(new GetGameRequest() { Id = id });
                return Ok(game.Game);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching Game Detail Id {Id}", id);
                return HandleException(ex, "Get");
            }
        }
       
       
        [HttpGet("GetGames/{storeId}")]
        public async Task<IActionResult> GetGames(int storeId)
        {
            try
            {
                var games = await mediator.Send(new GetAllGameRequest() {StoreId=storeId});
                return Ok(games.Games);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching Games Detail Id");
                return HandleException(ex, "GetAll");
            }
        }
        [Authorize(Policy = AuthPolicy.ClientAdmin)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InstanceGameUpdateVM game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addgame = await mediator.Send(new AddNewGameRequest() { Game = game, UserId = UserId });
                return Ok(addgame);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while adding Game  Detail.");
                return HandleException(ex, "Insert");
            }
        }

        //bulksave GameMaster
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] InstanceGameUpdateVM[] games)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var addgame = await mediator.Send(new GameRequest() { Games = games, UserId = UserId });
        //        return Ok(addgame);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex, "Error while adding or updating Game Detail.");
        //        return HandleException(ex, "Insert/Update");
        //    }
        //}

        [Authorize(Policy = AuthPolicy.ClientAdmin)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] InstanceGameUpdateVM game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addgame = await mediator.Send(new UpdateGameRequest() { Game = game, UserId = UserId });
                await unitOfWork.SaveChangesAsync();
                return Ok(addgame);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while updating Game  Detail.");
                return HandleException(ex, "Update");
            }
        }
        [Authorize(Policy = AuthPolicy.ClientAdmin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await mediator.Send(new DeleteGameRequest() { Id = id });
                await unitOfWork.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while deleting Game Detail Id {id}", id);
                return HandleException(new DeleteException("Error while deleting Game, may be used!"), "Delete");
            }
        }

    }
}
