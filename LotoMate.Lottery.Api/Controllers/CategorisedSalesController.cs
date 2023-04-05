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
    public class CategorisedSalesController : BaseController
    {
        private readonly ILogger<CategorisedSalesController> logger;
        private readonly IMediator mediator;
        private readonly IUnitOfWork<LotteryDbContext> unitOfWork;
        public CategorisedSalesController(ILogger<CategorisedSalesController> logger, IMediator mediator,
               IUnitOfWork<LotteryDbContext> unitOfWork, IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("GetTodaysSale/{storeId}")]
        public async Task<IActionResult> GetTodaysSale(int storeId)
        {
            try
            {
                var catSale = await mediator.Send(new GetCategorisedSalesRequest()
                {
                    StoreId = storeId,
                    TransactionDate = DateTime.Now,
                    UserId = UserId,
                    UserName = UserName
                });
                return Ok(catSale.CatSalesDetail);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching Categosrised Sale Detail User {UserId}", UserId);
                return HandleException(ex, "Get");
            }
        }
       

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategorisedSalesHeader catSale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var sale = await mediator.Send(new AddCategorisedSalesRequest() { CatSalesDetail = catSale, UserId = UserId });
                return Ok(sale);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while updating Categorised sales Detail.");
                return HandleException(ex, "Insert");
            }
        }
    }

}
