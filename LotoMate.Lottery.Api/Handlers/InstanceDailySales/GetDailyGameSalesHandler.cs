using AutoMapper;
using LotoMate.Exceptions;
using LotoMate.Lottery.Api.AutomapperProfiles;
using LotoMate.Lottery.Api.ViewModels;
using LotoMate.Lottery.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Api.Handlers.GameBook
{
    public class GetDailyGameSalesHandler : IRequestHandler<GetDailyGameSalesRequest, GetDailyGameSalesResponse>
    {
        private readonly ILogger<GetDailyGameSalesHandler> logger;
        private readonly IMapper mapper;
        private readonly IInstanceGameSalesRepository instanceGameSalesRepository;
        private readonly IInstanceGameBookRepository instanceGameBookRepository;
        public GetDailyGameSalesHandler(ILogger<GetDailyGameSalesHandler> logger, IMapper mapper, 
            IInstanceGameSalesRepository instanceGameSalesRepository, IInstanceGameBookRepository instanceGameBookRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.instanceGameSalesRepository = instanceGameSalesRepository;
            this.instanceGameBookRepository = instanceGameBookRepository;
        }
        public async Task<GetDailyGameSalesResponse> Handle(GetDailyGameSalesRequest request, CancellationToken cancellationToken)
         {
            try
            {
                //1. Day Open : Try and get the list from DailySalesTable, if already saved 
                //2. Day Close : Try and get the list from DailySalesTable, if already saved as opened
                var salesDetail = instanceGameSalesRepository.Queryable()
                                .Include(x=>x.Store)
                                .Include(u=>u.OpenUser)
                                .Include(u => u.ClosedUser)
                                .Include(x=>x.InstanceGameBook).ThenInclude(y=>y.InstanceGame)
                                .Where(x=> DateTime.Compare(x.TransactionDate.Value.Date,request.TransactionDate.Date)==0
                                           && x.StoreId == request.StoreId)
                                .OrderBy(x=>x.InstanceGameBook.DisplayNumber)
                                .ToList();
                if (salesDetail?.Count > 0)
                {
                    var fromDailySales = DailySlesMapper.IDStoIGSHeader(salesDetail);
                    return new GetDailyGameSalesResponse() { SalesDetail = fromDailySales };
                }
                //3 Get the list from Previous day, if not found in current day
                else
                {
                    var salesDetailPrev = instanceGameSalesRepository.Queryable()
                                .Include(x => x.Store)
                                .Include(u => u.OpenUser)
                                .Include(u => u.ClosedUser)
                                .Include(x => x.InstanceGameBook).ThenInclude(y => y.InstanceGame)
                                .Where(x => DateTime.Compare(x.TransactionDate.Value.Date,
                                                        request.TransactionDate.Date.AddDays(-1)) == 0
                                                        && x.StoreId == request.StoreId)
                                .OrderBy(x => x.InstanceGameBook.DisplayNumber)
                                .ToList();
                    if (salesDetailPrev?.Count > 0)
                    {
                        var fromDailySales = DailySlesMapper.IDStoIGSHeader(salesDetailPrev);
                        Parallel.ForEach(fromDailySales.SalesDetail, p =>
                         {
                             p.OpenNo = p.CloseNo;
                             p.CloseNo = p.CloseNo;
                             p.TotalSale = 0;
                             p.TotalSalePrice = 0;
                         });
                        return new GetDailyGameSalesResponse() { SalesDetail = fromDailySales };
                    }
                    else
                    {
                        //4 Get the fresh list from GamesBook table, if above gets failed
                        var bookDetail = instanceGameBookRepository.Queryable()
                                   .Include(x => x.Store)
                                   .Include(y => y.InstanceGame)
                                   .Where(x => x.IsActive.Value == true && x.StoreId == request.StoreId)
                                   .OrderBy(x => x.DisplayNumber)
                                   .ToList();
                        var fromBookDetail = DailySlesMapper.IGBtoIGSHeader(bookDetail,request.UserId, request.UserName);
                        return new GetDailyGameSalesResponse() { SalesDetail = fromBookDetail};
                    }
                }
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while retrieving Daily sales data, User : {UserId}", request.UserId);
                throw new InvalidParameterException("Error while retrieving Daily sales data.");
            }
        }
    }
    public class GetDailyGameSalesRequest : IRequest<GetDailyGameSalesResponse>
    {
        public DateTime TransactionDate { get; set; }
        public int StoreId { get; set; }
        public DailySaleState SaleState { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
    }
    public class GetDailyGameSalesResponse
    {
        public InstanceGameSalesHeader SalesDetail { get; set; }
    }
}
