using LotoMate.Exceptions;
using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Lottery.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Api.Handlers.Game
{
    public class DeleteGameMasterHandler : IRequestHandler<DeleteGameRequest>
    {
        private readonly ILogger<DeleteGameMasterHandler> logger;
        private readonly IInstanceGameRepository instanceGameRepository;
        public DeleteGameMasterHandler(ILogger<DeleteGameMasterHandler> logger,
                                 IInstanceGameRepository instanceGameRepository)
        {
            this.logger = logger;
            this.instanceGameRepository = instanceGameRepository;
        }
        public async Task<Unit> Handle(DeleteGameRequest request, CancellationToken cancellationToken)
        {
            var gameDetails = instanceGameRepository.Queryable().Where<InstanceGameMaster>(x => x.Id == request.Id).FirstOrDefault();
            if (gameDetails == null)
                throw new RecordNotFoundException("The Game details is not found for provided id.");
            instanceGameRepository.Delete(gameDetails);
            return await Unit.Task;
        }

        Task IRequestHandler<DeleteGameRequest>.Handle(DeleteGameRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public class DeleteGameRequest : IRequest
    {
        public int? Id { get; set; }
    }
}
