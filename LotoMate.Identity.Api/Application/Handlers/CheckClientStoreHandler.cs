using AutoMapper;
using LotoMate.Identity.API.ViewModels;
using LotoMate.Identity.Infrastructure.Models;
using LotoMate.Identity.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Identity.API.Handlers
{
    public class CheckClientStoreHandler : IRequestHandler<GetClientStoreRequest, GetClientStoreResponse>
    {
        private readonly ILogger<CheckClientStoreHandler> logger;
        private readonly IMapper mapper;
        private readonly IClientRepository clientRepository;

        public CheckClientStoreHandler(ILogger<CheckClientStoreHandler> logger, IMapper mapper, IClientRepository clientRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.clientRepository = clientRepository;
        }

        public async Task<GetClientStoreResponse> Handle(GetClientStoreRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Add new client, Return existing if it finds
                var client = clientRepository.Queryable().Include(c => c.Stores)
                                .Where(x => x.Id == request.ClientId && x.Stores.Any(y=>y.Id == request.StoreId))
                                .FirstOrDefault();
                                //return result
                return new GetClientStoreResponse() { 
                    Exists = client!= null};
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while querying Client!, userid :  {UserId}", request.UserId);
                throw;
            }
        }

    }

    public class GetClientStoreRequest : IRequest<GetClientStoreResponse>
    {
        public int ClientId { get; set; }
        public int StoreId { get; set; }
        public int? UserId { get; set; }
    }

    public class GetClientStoreResponse
    {
        public bool Exists { get; set; }
    }
}
