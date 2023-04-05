using AutoMapper;
using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Lottery.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Api.Handlers.Game
{
    public class AddNewGameHandler : IRequestHandler<AddNewGameRequest, AddNewGameResponse>
    {
        private readonly ILogger<AddNewGameHandler> logger;
        private readonly IMapper mapper;
        private readonly IInstanceGameRepository instanceGameRepository;
        public AddNewGameHandler(ILogger<AddNewGameHandler> logger, IMapper mapper, IInstanceGameRepository instanceGameRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.instanceGameRepository = instanceGameRepository;
        }
        public async Task<AddNewGameResponse> Handle(AddNewGameRequest request, CancellationToken cancellationToken)
        {
            var game = mapper.Map<InstanceGameMaster>(request.Game );
            game = await instanceGameRepository.AddAndSave(game);
            return new AddNewGameResponse() { Game = mapper.Map<InstanceGameUpdateVM>(game) };
        }
    }
    public class AddNewGameRequest : IRequest<AddNewGameResponse>
    {
        public InstanceGameUpdateVM Game { get; set; }
        public int? UserId { get; set; }
    }
    public class AddNewGameResponse
    {
        public InstanceGameUpdateVM Game { get; set; }
    }
}
