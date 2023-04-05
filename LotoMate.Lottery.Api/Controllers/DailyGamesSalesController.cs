using LotoMate.Framework;
using LotoMate.Framework.Authorisation;
using LotoMate.Lottery.Api.Handlers.CategorisedSales;
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
    public class DailyGamesSalesController : BaseController
    {
        private readonly ILogger<DailyGamesSalesController> logger;
        private readonly IMediator mediator;
        private readonly IUnitOfWork<LotteryDbContext> unitOfWork;

        public DailyGamesSalesController(ILogger<DailyGamesSalesController> logger, IMediator mediator, 
                IUnitOfWork<LotteryDbContext> unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("GetTodaysSale/{storeId}/{saleState}")]
        public async Task<IActionResult> GetTodaysSale(int storeId, DailySaleState saleState)
        {
            try
            {
                var gameSale = await mediator.Send(new GetDailyGameSalesRequest()
                {
                    StoreId = storeId,
                    SaleState = saleState,
                    TransactionDate = DateTime.Now,
                    UserId = UserId,
                    UserName = UserName
                });
                return Ok(gameSale.SalesDetail);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching Game Sale Detail User {UserId}", UserId);
                return HandleException(ex, "Get");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InstanceGameSalesHeader instanceGameSale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var gameSale = await mediator.Send(new AddDailyGameSalesRequest() { GameSales = instanceGameSale, UserId = UserId });
                return Ok(gameSale);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while updating Game sales Detail.");
                return HandleException(ex, "Insert");
            }
        }







    }
}
