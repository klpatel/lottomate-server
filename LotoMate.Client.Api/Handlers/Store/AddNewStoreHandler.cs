using AutoMapper;
using LotoMate.Client.Infrastructure.Models;
using LotoMate.Client.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Client.Api.Handlers
{
    public class AddNewStoreHandler : IRequestHandler<AddNewStoreRequest, AddNewStoreResponse>
    {
        private readonly ILogger<AddNewStoreHandler> logger;
        private readonly IMapper mapper;
        private readonly IStoreRepository storeRepository;

        public AddNewStoreHandler(ILogger<AddNewStoreHandler> logger, IMapper mapper, IStoreRepository storeRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.storeRepository = storeRepository;
        }

        public async Task<AddNewStoreResponse> Handle(AddNewStoreRequest request, CancellationToken cancellationToken)
        {
            var store = mapper.Map<Store>(request.Store);
            storeRepository.Insert(store);

            return new AddNewStoreResponse();
        }

    }

    public class AddNewStoreRequest : IRequest<AddNewStoreResponse>
    {
        public StoreViewModel Store { get; set; }
        public int UserId { get; set; }
    }

    public class AddNewStoreResponse
    {

    }
}
