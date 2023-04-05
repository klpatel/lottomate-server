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
    public class GetCatSalesHandler : IRequestHandler<GetCatSalesRequest, GetCatSalesResponse>
    {
        private readonly ILogger<GetCatSalesHandler> logger;
        private readonly IMapper mapper;
        private readonly ICategorySalesRepository categorySalesRepository;
        public GetCatSalesHandler(ILogger<GetCatSalesHandler> logger, IMapper mapper,
            ICategorySalesRepository categorySalesRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.categorySalesRepository = categorySalesRepository;
        }
        public async Task<GetCatSalesResponse> Handle(GetCatSalesRequest request, CancellationToken cancellationToken)
         {
            try
            {
                var salesDetail = categorySalesRepository.Queryable()
                                .Include(x=>x.Store)
                                .Include(u=>u.GameSalesCategory)
                                .Where(x=> DateTime.Compare(x.TransactionDate.Value.Date,request.TransactionDate.Date)==0
                                           && x.StoreId == request.StoreId)
                                .OrderBy(x=>x.GameSalesCategory.DebitOrCredit)
                                .ToList();
                if (salesDetail?.Count > 0)
                {
                    var fromDailySales = DailySalesMapper.CatSaletoCatHeader(salesDetail);
                    return new GetCatSalesResponse() { SalesDetail = fromDailySales };
                }
                //throw exception in case of no records found
                throw new RecordNotFoundException("No records found for the storeid and/or the date!");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while retrieving Categorised Daily sales data, User : {UserId}", request.UserId);
                throw new InvalidParameterException("Error while retrieving Categorised Daily sales data.");
            }
        }
    }
    public class GetCatSalesRequest : IRequest<GetCatSalesResponse>
    {
        public DateTime TransactionDate { get; set; }
        public int StoreId { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
    }
    public class GetCatSalesResponse
    {
        public CategorySalesHeader SalesDetail { get; set; }
    }
}
