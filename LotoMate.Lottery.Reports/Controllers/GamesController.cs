using LotoMate.Exceptions;
using LotoMate.Framework.Authorisation;
using LotoMate.Lottery.Reports.Handlers;
using LotoMate.Lottery.Reports.ReportData;
using LotoMate.Reports.Library.PdfProcessor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Reports.Controllers
{
    [Authorize(Policy = AuthPolicy.ClientUser)]
    //[AllowAnonymous]
    public class SalesReportController : BaseController
    {
        private readonly ILogger<SalesReportController> logger;
        private readonly IMediator mediator;
        private readonly IPdfProcessor pdfProcessor;
        public SalesReportController(ILogger<SalesReportController> logger, IMediator mediator, 
                IPdfProcessor pdfProcessor, IHttpContextAccessor httpContextAccessor) 
                : base(logger,httpContextAccessor)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.pdfProcessor = pdfProcessor;
        }

        [HttpGet("InstanceSale/{storeId}/{date}")]
        //[AllowAnonymous]
        public async Task<IActionResult> InstanceSale(int storeId, DateTime date)
        {
            try
            {
                //get the report data
                var sale = mediator.Send(new GetInstanceSalesRequest() { StoreId = storeId, TransactionDate = date, UserId = UserId }).Result;
                
                //prepare report 
                var reportData = new InstanceSalesData(logger);
                var report = reportData.GetDetails(sale.SalesDetail);

                //generate report
                var stream= await pdfProcessor.GenerateStream(new SalesTemplateGenerator(report));
                //var pdf = pdfProcessor.Generate(new SalesTemplateGenerator(report));

                //write to stream
                //return new PhysicalFileResult(pdf, "application/pdf");
                return new FileStreamResult(stream, "application/pdf");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching report!");
                return HandleException(ex, "Get");
                //return null;
            }
        }

        [HttpGet("CategorySale/{storeId}/{date}")]
        public async Task<IActionResult> CategorySale(int storeId, DateTime date)
        {
            try
            {
                //get the report data
                var sale = await mediator.Send(new GetCatSalesRequest() { StoreId = storeId, TransactionDate = date, UserId = UserId });
                //prepare report 
                var reportData = new CategorySalesData(logger);
                var report = reportData.GetDetails(sale.SalesDetail);
                //generate report
                var stream = await pdfProcessor.GenerateStream(new SalesTemplateGenerator(report));
                //var pdf = pdfProcessor.Generate(new SalesTemplateGenerator(report));

                //write to stream
                //return new PhysicalFileResult(pdf, "application/pdf");
                return new FileStreamResult(stream, "application/pdf");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching report!");
                return HandleException(ex, "Get");
                //return null;
            }
        }

        [HttpGet("download/{reportFile}")]
        public async Task<IActionResult> download(string reportFile)
        {
            try
            {
                //var games = await mediator.Send(new GetAllGameRequest() {});
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while dowloading report!");
                return HandleException(ex, "Download");
            }
        }
    }
}
