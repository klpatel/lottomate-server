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


namespace LotoMate.Lottery.Api.Handlers.CategorisedSales
{
    public class AddCategorisedSalesHandler : IRequestHandler<AddCategorisedSalesRequest, AddCategorisedSalesResponse>
    {
        private readonly ILogger<GetCategorisedSalesHandler> logger;
        private readonly IMapper mapper;
        private readonly IGameSalesCategoryRepository gameSalesCategoryRepository;
        private readonly ICategorySalesRepository categorySalesRepository;

        public AddCategorisedSalesHandler(ILogger<GetCategorisedSalesHandler> logger, IMapper mapper,
           IGameSalesCategoryRepository gameSalesCategoryRepository, ICategorySalesRepository categorySalesRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.gameSalesCategoryRepository = gameSalesCategoryRepository;
            this.categorySalesRepository =  categorySalesRepository;
        }
        public async Task<AddCategorisedSalesResponse> Handle(AddCategorisedSalesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var catSale = CategorisedSalesProfile.CSHeadertoGS(request.CatSalesDetail);
                Parallel.ForEach(catSale, p =>
                {
                    if (p.Id == 0)
                        p.AuditAdd(request.UserId);
                    else
                        p.AuditUpdate(request.UserId);
                });
                await  categorySalesRepository.BulkInsert(catSale.Where(x => x.Id == 0).ToList());
                await categorySalesRepository.BulkUpdate(catSale.Where(x => x.Id != 0).ToList());
                return new AddCategorisedSalesResponse();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while saving Categorised sales data, User : {UserId}", request.UserId);
                throw new InvalidParameterException("Error while saving Categorised sales data.");
            }
        }
    }
        public class AddCategorisedSalesRequest : IRequest<AddCategorisedSalesResponse>
        {
            public CategorisedSalesHeader CatSalesDetail { get; set; }
            public int? UserId { get; set; }
        }
        public class AddCategorisedSalesResponse
        {

        }
    }
