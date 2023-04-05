using AutoMapper;
using LotoMate.Exceptions;
using LotoMate.Lottery.Infrastructure.Repositories;
using LotoMate.Lottery.Reports.AutomapperProfiles;
using LotoMate.Lottery.Reports.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Reports.Handlers
{
    public class GetInstanceSalesHandler : IRequestHandler<GetInstanceSalesRequest, GetInstanceSalesResponse>
    {
        private readonly ILogger<GetInstanceSalesHandler> logger;
        private readonly IMapper mapper;
        private readonly IInstanceGameSalesRepository instanceGameSalesRepository;
        public GetInstanceSalesHandler(ILogger<GetInstanceSalesHandler> logger, IMapper mapper, 
            IInstanceGameSalesRepository instanceGameSalesRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.instanceGameSalesRepository = instanceGameSalesRepository;
        }
        public async Task<GetInstanceSalesResponse> Handle(GetInstanceSalesRequest request, CancellationToken cancellationToken)
         {
            try
            {
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
                    var fromDailySales = DailySalesMapper.IDStoIGSHeader(salesDetail);
                    return new GetInstanceSalesResponse() { SalesDetail = fromDailySales };
                }
                //throw exception in case of no records found
                throw new RecordNotFoundException("No records found for the storeid and/or the date!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while retrieving Daily sales data, User : {UserId}", request.UserId);
                throw new InvalidParameterException("Error while retrieving Daily sales data.");
            }
        }
    }
    public class GetInstanceSalesRequest : IRequest<GetInstanceSalesResponse>
    {
        public DateTime TransactionDate { get; set; }
        public int StoreId { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
    }
    public class GetInstanceSalesResponse
    {
        public InstanceSalesHeader SalesDetail { get; set; }
    }
}
