using AutoMapper;
using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Lottery.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Api.Handlers.IStore
{
    public class AddNewStoreHandler : IRequestHandler<AddNewStoreRequest, AddNewStoreResponse>
    {
        private readonly ILogger<AddNewStoreHandler> logger;
        private readonly IMapper mapper;
        private readonly IStoreRepository  storeRepository;
        public AddNewStoreHandler(ILogger<AddNewStoreHandler> logger, IMapper mapper, IStoreRepository storeRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.storeRepository = storeRepository;
        }
        public async Task<AddNewStoreResponse> Handle(AddNewStoreRequest request, CancellationToken cancellationToken)
        {
            var Store = mapper.Map<Store>(request.Store );
            Store = await storeRepository.AddAndSave(Store);
            return new AddNewStoreResponse() { Store = mapper.Map<StoreVM>(Store) };
        }
    }
    public class AddNewStoreRequest : IRequest<AddNewStoreResponse>
    {
        public StoreVM Store { get; set; }
        public int? UserId { get; set; }
    }
    public class AddNewStoreResponse
    {
        public StoreVM Store { get; set; }
    }
}
