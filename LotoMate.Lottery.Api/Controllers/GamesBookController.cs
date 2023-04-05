using LotoMate.Framework;
using LotoMate.Framework.Authorisation;
using LotoMate.Lottery.Api.Handlers.GameBook;
using LotoMate.Lottery.Api.ViewModels;
using LotoMate.Lottery.Infrastructure;
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
    public class GamesBookController : BaseController
    {
        private readonly ILogger<GamesBookController> logger;
        private readonly IMediator mediator;
        private readonly IUnitOfWork<LotteryDbContext> unitOfWork;

        public GamesBookController(ILogger<GamesBookController> logger, IMediator mediator, 
            IUnitOfWork<LotteryDbContext> unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GamesBook(int id,int storeid)
        {
            try
            {
                var gameBook = await mediator.Send(new GetGameBookRequest() { Id = id,StoreId=storeid });
                return Ok(gameBook.GameBook);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching Game Book Detail Id {Id}", id);
                return HandleException(ex, "Get");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GamesBook(int storeid)
        {
            try
            {
                var games = await mediator.Send(new GetAllGameBookRequest() {StoreId=storeid });
                return Ok(games.GamesBook);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching Games Book Detail");
                return HandleException(ex, "GetAll");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameBookUpdateModel gameBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var gamebook = await mediator.Send(new AddNewGameBookRequest() { GameBook = gameBook , UserId = UserId });
                return Ok(gamebook);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while adding Game Book Detail.");
                return HandleException(ex, "Insert");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] GameBookUpdateModel gameBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var gamebook = await mediator.Send(new UpdateGameBookRequest() { GameBook = gameBook, UserId = UserId });
                await unitOfWork.SaveChangesAsync();
                return Ok(gamebook);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while updateing Game Book Detail.");
                return HandleException(ex, "Update");
            }
        }
    }
}
