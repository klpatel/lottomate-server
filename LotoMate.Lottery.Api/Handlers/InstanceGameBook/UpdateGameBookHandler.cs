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

namespace LotoMate.Lottery.Api.Handlers.GameBook
{
    public class UpdateGameBookHandler : IRequestHandler<UpdateGameBookRequest, UpdateGameBookResponse>
    {
        private readonly ILogger<UpdateGameBookHandler> logger;
        private readonly IMapper mapper;
        private readonly IInstanceGameBookRepository instanceGameBookRepository;
        public UpdateGameBookHandler(ILogger<UpdateGameBookHandler> logger, IMapper mapper, IInstanceGameBookRepository instanceGameBookRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.instanceGameBookRepository = instanceGameBookRepository;
        }
        public async Task<UpdateGameBookResponse> Handle(UpdateGameBookRequest request, CancellationToken cancellationToken)
        {
            var gameBook = mapper.Map<InstanceGameBook>(request.GameBook);
            instanceGameBookRepository.Update(gameBook);
            return new UpdateGameBookResponse() { GameBook = mapper.Map<GameBookUpdateModel>(gameBook) };
        }
    }
    public class UpdateGameBookRequest : IRequest<UpdateGameBookResponse>
    {
        public GameBookUpdateModel GameBook  { get; set; }
        public int? UserId { get; set; }
    }
    public class UpdateGameBookResponse
    {
        public GameBookUpdateModel GameBook { get; set; }
    }
}
