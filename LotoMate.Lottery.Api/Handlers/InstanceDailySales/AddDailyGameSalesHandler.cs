using AutoMapper;
using LotoMate.Exceptions;
using LotoMate.Lottery.Api.AutomapperProfiles;
using LotoMate.Lottery.Api.ViewModels;
using LotoMate.Lottery.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Api.Handlers.GameBook
{
    public class AddDailyGameSalesHandler : IRequestHandler<AddDailyGameSalesRequest, AddDailyGameSalesResponse>
    {
        private readonly ILogger<AddDailyGameSalesHandler> logger;
        private readonly IMapper mapper;
        private readonly IInstanceGameSalesRepository instanceGameSalesRepository;
        public AddDailyGameSalesHandler(ILogger<AddDailyGameSalesHandler> logger, IMapper mapper, IInstanceGameSalesRepository instanceGameSalesRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.instanceGameSalesRepository = instanceGameSalesRepository;
        }
        public async Task<AddDailyGameSalesResponse> Handle(AddDailyGameSalesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.GameSales.SaleState == DailySaleState.Open)
                {
                    request.GameSales.OpenUserId = request.UserId;
                    request.GameSales.ClosedUserId = null;
                }
                else if (request.GameSales.SaleState == DailySaleState.Open)
                {
                    request.GameSales.ClosedUserId = request.UserId;
                }

                var dailySale = DailySlesMapper.IGSHeadertoIDS(request.GameSales);
                Parallel.ForEach(dailySale, p =>
                 {
                     if (p.Id == 0)
                         p.AuditAdd(request.UserId);
                     else
                         p.AuditUpdate(request.UserId);
                 });
                await instanceGameSalesRepository.BulkInsert(dailySale.Where(x => x.Id == 0).ToList());
                await instanceGameSalesRepository.BulkUpdate(dailySale.Where(x => x.Id != 0).ToList());
                return new AddDailyGameSalesResponse();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while saving Daily sales data, User : {UserId}", request.UserId);
                throw new InvalidParameterException("Error while saving Daily sales data.");
            }
        }
    }
    public class AddDailyGameSalesRequest : IRequest<AddDailyGameSalesResponse>
    {
        public InstanceGameSalesHeader GameSales { get; set; }
        public int? UserId { get; set; }
    }
    public class AddDailyGameSalesResponse
    {

    }
}
