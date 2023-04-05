using AutoMapper;
using LotoMate.Exceptions;
using LotoMate.Lottery.Api.AutomapperProfiles;
using LotoMate.Lottery.Api.ViewModels;
using LotoMate.Lottery.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Api.Handlers.CategorisedSales
{
    public class GetCategorisedSalesHandler : IRequestHandler<GetCategorisedSalesRequest, GetCategorisedSalesResponse>
    {
        private readonly ILogger<GetCategorisedSalesHandler> logger;
        private readonly IMapper mapper;
        private readonly IGameSalesCategoryRepository gameSalesCategoryRepository;
        private readonly ICategorySalesRepository categorySalesRepository;
        private readonly IInstanceGameSalesRepository instanceGameSalesRepository;

        public GetCategorisedSalesHandler(ILogger<GetCategorisedSalesHandler> logger, IMapper mapper,
           IGameSalesCategoryRepository gameSalesCategoryRepository, 
           ICategorySalesRepository categorySalesRepository,
           IInstanceGameSalesRepository instanceGameSalesRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.gameSalesCategoryRepository = gameSalesCategoryRepository;
            this.categorySalesRepository = categorySalesRepository;
            this.instanceGameSalesRepository = instanceGameSalesRepository;
        }
        public async Task<GetCategorisedSalesResponse> Handle(GetCategorisedSalesRequest request, CancellationToken cancellationToken)
        {

            try
            { 
                //1. Try and get the list from CategorisedSales, if already saved 
                var salesCat = categorySalesRepository.Queryable()
                               .Include(x => x.GameSalesCategory)
                               .Include(y => y.Store)
                                .Where(x => DateTime.Compare(x.TransactionDate.Value.Date,
                                       request.TransactionDate.Date) == 0 && 
                                        x.StoreId == request.StoreId
                                        && x.GameSalesCategory.IsActive == true)
                                .ToList();
                if(salesCat.Count > 0)
                {
                    var fromSalesCat = CategorisedSalesProfile.GStoCSHeader(salesCat);
                    //calculating and assigning totalprice from InstanceDailySale
                    GetTotalPrice(request.TransactionDate, fromSalesCat.DebitSalesDetail);
                    return new GetCategorisedSalesResponse() { CatSalesDetail = fromSalesCat };
                }
                 
                //2. Get the fresh list from GameSalesCategory table, if above gets failed
                var gameSalesCat = gameSalesCategoryRepository.Queryable()
                           .Include(x => x.Store)
                           .Where(x => x.IsActive.Value == true && x.StoreId == request.StoreId)
                           .ToList();
                var fromGameSalesCat = CategorisedSalesProfile.GSCtoCSHeader(gameSalesCat);
                //calculating and assigning totalprice from InstanceDailySale
                GetTotalPrice(request.TransactionDate, fromGameSalesCat.DebitSalesDetail);
                return new GetCategorisedSalesResponse() { CatSalesDetail = fromGameSalesCat };
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error while retrieving Categorised sales data, User : {UserId}", request.UserId);
                throw new InvalidParameterException("Error while retrieving Categorised sales data.");
            }

        }

        public void GetTotalPrice(DateTime trDate, ICollection<CategorisedSalesViewModel> SalesDetail)
        {
            //getting Online Gross sale from InstanceDailySales
            var totalPrice = instanceGameSalesRepository.Queryable()
                             .Where(x => DateTime.Compare(x.TransactionDate.Value.Date,
                                       trDate.Date) == 0)
                             .Sum(x => (x.Price) * (x.CloseNo - x.OpenNo));
            var grossSale = SalesDetail .Where(x => x.CategoryName.ToLower() == ("Instance Gross Sale").ToLower()).FirstOrDefault();
            grossSale.Total = totalPrice;
        }

    }
    public class GetCategorisedSalesRequest : IRequest<GetCategorisedSalesResponse>
    {
        public DateTime TransactionDate { get; set; }
        public int StoreId { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
    }
    public class GetCategorisedSalesResponse
    {
       public  CategorisedSalesHeader CatSalesDetail { get; set; }
    }

    
}
