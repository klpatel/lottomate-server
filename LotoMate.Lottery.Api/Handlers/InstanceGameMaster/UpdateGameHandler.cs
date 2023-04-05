using AutoMapper;
using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Lottery.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Api.Handlers.Game
{
    public class UpdateGameHandler : IRequestHandler<UpdateGameRequest, UpdateGameResponse>
    {
        private readonly ILogger<UpdateGameHandler> logger;
        private readonly IMapper mapper;
        private readonly IInstanceGameRepository instanceGameRepository;
        public UpdateGameHandler(ILogger<UpdateGameHandler> logger, IMapper mapper, IInstanceGameRepository instanceGameRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.instanceGameRepository = instanceGameRepository;
        }
        public async Task<UpdateGameResponse> Handle(UpdateGameRequest request, CancellationToken cancellationToken)
        {
            var game = mapper.Map<InstanceGameMaster>(request.Game );
            instanceGameRepository.Update(game);
            return new UpdateGameResponse() { Game = mapper.Map<InstanceGameUpdateVM>(game) };
        }
    }
    public class UpdateGameRequest : IRequest<UpdateGameResponse>
    {
        public InstanceGameUpdateVM Game { get; set; }
        public int? UserId { get; set; }
    }
    public class UpdateGameResponse
    {
        public InstanceGameUpdateVM Game { get; set; }
    }
}
