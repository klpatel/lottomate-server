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
    public class GetGameBookHandler : IRequestHandler<GetGameBookRequest, GetGameBookResponse>
    {
        private readonly ILogger<GetGameBookHandler> logger;
        private readonly IMapper mapper;
        private readonly IInstanceGameBookRepository instanceGameBookRepository;
        public GetGameBookHandler(ILogger<GetGameBookHandler> logger, IMapper mapper, IInstanceGameBookRepository instanceGameBookRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.instanceGameBookRepository = instanceGameBookRepository;
        }
        public async Task<GetGameBookResponse> Handle(GetGameBookRequest request, CancellationToken cancellationToken)
        {
            var gameBook = instanceGameBookRepository.Queryable()
                                .Where<InstanceGameBook>(x => x.Id == request.Id && x.StoreId == request.StoreId)
                                .FirstOrDefault();
            if (gameBook == null)
                throw new RecordNotFoundException("The GameBook details is not found for provided id.");

            var gameBookVM = mapper.Map<InstanceGameBookViewModel>(gameBook);
            return new GetGameBookResponse() { GameBook = gameBookVM };
        }
    }
    public class GetGameBookRequest : IRequest<GetGameBookResponse>
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }
    }

    public class GetGameBookResponse
    {
        public InstanceGameBookViewModel GameBook { get; set; }
    }
}
