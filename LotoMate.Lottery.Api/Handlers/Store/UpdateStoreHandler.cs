using AutoMapper;
using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Lottery.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Api.Handlers.IStore
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
            var Store = mapper.Map<Store>(request.Store );
            storeRepository.Update(Store);
            return new UpdateStoreResponse() { Store = mapper.Map<StoreVM>(Store) };
        }
    }
    public class UpdateStoreRequest : IRequest<UpdateStoreResponse>
    {
        public StoreVM Store { get; set; }
        public int? UserId { get; set; }
    }
    public class UpdateStoreResponse
    {
        public StoreVM Store { get; set; }
    }
}
