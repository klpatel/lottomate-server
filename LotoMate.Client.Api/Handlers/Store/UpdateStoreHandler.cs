using AutoMapper;
using LotoMate.Client.Infrastructure.Models;
using LotoMate.Client.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Client.Api.Handlers
{
    public class UpdateStoreHandler : IRequestHandler<UpdateStoreRequest, UpdateStoreResponse>
    {
        private readonly ILogger<UpdateStoreHandler> logger;
        private readonly IMapper mapper;
        private readonly IStoreRepository storeRepository;

        public UpdateStoreHandler(ILogger<UpdateStoreHandler> logger, IMapper mapper, IStoreRepository storeRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.storeRepository = storeRepository;
        }

        public async Task<UpdateStoreResponse> Handle(UpdateStoreRequest request, CancellationToken cancellationToken)
        {
            var store = mapper.Map<Store>(request.Store);
            storeRepository.Update(store);

            return new UpdateStoreResponse();
        }

    }

    public class UpdateStoreRequest : IRequest<UpdateStoreResponse>
    {
        public StoreViewModel Store { get; set; }
        public int UserId { get; set; }
    }

    public class UpdateStoreResponse
    {

    }
}
