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
    public class AddNewGameBookHandler : IRequestHandler<AddNewGameBookRequest, AddNewGameBookResponse>
    {
        private readonly ILogger<AddNewGameBookHandler> logger;
        private readonly IMapper mapper;
        private readonly IInstanceGameBookRepository instanceGameBookRepository;
        public AddNewGameBookHandler(ILogger<AddNewGameBookHandler> logger, IMapper mapper, IInstanceGameBookRepository instanceGameBookRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.instanceGameBookRepository = instanceGameBookRepository;
        }
        public async Task<AddNewGameBookResponse> Handle(AddNewGameBookRequest request, CancellationToken cancellationToken)
        {
            var gameBook = mapper.Map<InstanceGameBook>(request.GameBook);
            gameBook = await instanceGameBookRepository.AddAndSave(gameBook);
            return new AddNewGameBookResponse() { GameBook = mapper.Map<GameBookUpdateModel>(gameBook) };
        }
    }
    public class AddNewGameBookRequest : IRequest<AddNewGameBookResponse>
    {
        public GameBookUpdateModel GameBook  { get; set; }
        public int? UserId { get; set; }
    }
    public class AddNewGameBookResponse
    {
        public GameBookUpdateModel GameBook { get; set; }
    }
}
