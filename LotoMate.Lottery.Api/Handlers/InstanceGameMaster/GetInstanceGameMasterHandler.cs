using AutoMapper;
using LotoMate.Exceptions;
using LotoMate.Lottery.Api.ViewModels;
using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Lottery.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace LotoMate.Lottery.Api.Handlers.GameMaster
{
    public class GetInstanceGameMasterHandler : IRequestHandler<GetGameRequest, GetGameResponse>
    {
        private readonly ILogger<GetInstanceGameMasterHandler> logger;
        private readonly IMapper mapper;
        private readonly IInstanceGameRepository instanceGameRepository;

        public GetInstanceGameMasterHandler(ILogger<GetInstanceGameMasterHandler> logger, IMapper mapper, IInstanceGameRepository instanceGameRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.instanceGameRepository = instanceGameRepository;
        }
        public async Task<GetGameResponse> Handle(GetGameRequest request, CancellationToken cancellationToken)
        {
            var game = instanceGameRepository.Queryable()
                                .Where<InstanceGameMaster>(x => x.Id == request.Id).FirstOrDefault();
            if (game == null)
                throw new RecordNotFoundException("The Game details is not found for provided id.");

            var gameVM = mapper.Map<InstanceGameMasterViewModel>(game);
            return new GetGameResponse() { Game = gameVM };
        }
    }
    public class GetGameRequest : IRequest<GetGameResponse>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }

    public class GetGameResponse
    {
        public InstanceGameMasterViewModel Game { get; set; }
    }
}
