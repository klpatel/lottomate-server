using AutoMapper;
using LotoMate.Exceptions;
using LotoMate.Identity.Api.ViewModels;
using LotoMate.Identity.Infrastructure.Models;
using LotoMate.Identity.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace LotoMate.Identity.Api.Handlers
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
            var stores = storeRepository.Queryable()
                            .Include(x=>x.AspNetUserRoles)
                            .Where<Store>(x => x.ClientId == request.ClientId
                             && x.AspNetUserRoles.Any(y=>y.UserId == request.UserId))
                            .ToList();
            if (stores?.Count == 0)
                throw new RecordNotFoundException("The Stores details are not found for provided id.");

            var storesVM = mapper.Map<IList<StoreViewModel>>(stores);
            return new GetStoreResponse() {  Stores = storesVM };
        }
    }
    public class GetStoreRequest : IRequest<GetStoreResponse>
    {
        public int ClientId { get; set; }
        public int? UserId { get; set; }
    }

    public class GetStoreResponse
    {
        public IList<StoreViewModel> Stores { get; set; }
    }
}
