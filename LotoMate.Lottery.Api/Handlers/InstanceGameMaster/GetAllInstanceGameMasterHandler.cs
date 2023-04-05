using AutoMapper;
using AutoMapper.QueryableExtensions;
using LotoMate.Exceptions;
using LotoMate.Lottery.Api.ViewModels;
using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Lottery.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Api.Handlers.GameMaster
{
    public class GetAllGameMasterHandler : IRequestHandler<GetAllGameRequest, GetAllGameResponse>
    {
        private readonly ILogger<GetAllGameMasterHandler> logger;
        private readonly IMapper mapper;
        private readonly IInstanceGameRepository instanceGameRepository;

        public GetAllGameMasterHandler(ILogger<GetAllGameMasterHandler> logger, IMapper mapper, IInstanceGameRepository instanceGameRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.instanceGameRepository = instanceGameRepository;
        }
        public async Task<GetAllGameResponse> Handle(GetAllGameRequest request, CancellationToken cancellationToken)
        {
            var games = instanceGameRepository.Queryable().Where(x=> x.StoreId == request.StoreId);

            var gamesVM = games.ProjectTo<InstanceGameMasterViewModel>
                                              (mapper.ConfigurationProvider).ToList();
            return new GetAllGameResponse() { Games = gamesVM };
        }
    }
    public class GetAllGameRequest : IRequest<GetAllGameResponse>
    {
        public int StoreId { get; set; }
        public int UserId { get; set; }
    }

    public class GetAllGameResponse
    {
        public IEnumerable<InstanceGameMasterViewModel> Games { get; set; }
    }
}
