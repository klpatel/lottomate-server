using AutoMapper;
using LotoMate.Client.Api.ViewModels;
using LotoMate.Client.Infrastructure.Models;
using LotoMate.Client.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Client.Api.Handlers
{
    public class GetStoreHandler : IRequestHandler<GetStoreRequest, GetStoreResponse>
    {
        private readonly ILogger<GetStoreHandler> logger;
        private readonly IMapper mapper;
        private readonly IStoreRepository storeRepository;

        public GetStoreHandler(ILogger<GetStoreHandler> logger, IMapper mapper, IStoreRepository storeRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.storeRepository = storeRepository;
        }

        public async Task<GetStoreResponse> Handle(GetStoreRequest request, CancellationToken cancellationToken)
        {
            
            var store = storeRepository.Queryable().Where<Store>(x=>x.Id == request.Id).FirstOrDefault();
            var storeVM = mapper.Map<StoreViewModel>(store);
            return new GetStoreResponse() {  Store = storeVM};
        }

    }

    public class GetStoreRequest : IRequest<GetStoreResponse>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }

    public class GetStoreResponse
    {
      public  StoreViewModel  Store { get; set; } 
    }
}
