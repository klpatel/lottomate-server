using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Lottery.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Lottery.Api.Handlers.Game
{
    public class BulkSaveGameMasterHandler : IRequestHandler<GameRequest, GameResponse>
    {
        private readonly ILogger<BulkSaveGameMasterHandler> logger;
        private readonly IInstanceGameRepository instanceGameRepository;
        public BulkSaveGameMasterHandler(ILogger<BulkSaveGameMasterHandler> logger, IInstanceGameRepository instanceGameRepository)
        {
            this.logger = logger;
            this.instanceGameRepository = instanceGameRepository;
        }
        public async Task<GameResponse> Handle(GameRequest request, CancellationToken cancellationToken)
        {
           

            var games = request.Games.Select(x =>
                  new InstanceGameMaster()
                  {
                      Id = x.Id,
                      StoreId = x.StoreId,
                      GameNumber = x.GameNumber,
                      Name = x.Name,
                      Price = x.Price,
                      IsActive = x.IsActive,
                      BookTotal = x.BookTotal,

                  }).ToList();

            //auditing fields
            Parallel.ForEach(games, p =>
            {
                if (p.Id == 0)
                    p.AuditAdd(request.UserId);
                else
                    p.AuditUpdate(request.UserId);
            });

            await instanceGameRepository.BulkInsert(games.Where(x => x.Id == 0).ToList());
            await instanceGameRepository.BulkUpdate(games.Where(x => x.Id != 0).ToList());
            return new GameResponse();
        }

    }

    public class GameRequest : IRequest<GameResponse>
    {
        public ICollection<InstanceGameUpdateVM> Games { get; set; }
        public int? UserId { get; set; }
    }
    public class GameResponse
    {

    }
}
