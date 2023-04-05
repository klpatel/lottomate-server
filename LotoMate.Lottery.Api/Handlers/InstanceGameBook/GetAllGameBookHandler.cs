using AutoMapper;
using AutoMapper.QueryableExtensions;
using LotoMate.Exceptions;
using LotoMate.Lottery.Api.ViewModels;
using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Lottery.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LotoMate.Lottery.Api.Handlers.GameBook
{
    public class GetAllGameBookHandler : IRequestHandler<GetAllGameBookRequest, GetAllGameBookResponse>
    {
        private readonly ILogger<GetAllGameBookHandler> logger;
        private readonly IMapper mapper;
        private readonly IInstanceGameBookRepository instanceGameBookRepository;
        public GetAllGameBookHandler(ILogger<GetAllGameBookHandler> logger, IMapper mapper, IInstanceGameBookRepository instanceGameBookRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.instanceGameBookRepository = instanceGameBookRepository;
        }
        public async Task<GetAllGameBookResponse> Handle(GetAllGameBookRequest request, CancellationToken cancellationToken)
        {
            var gamesBook = instanceGameBookRepository.Queryable()
                            .Include(x => x.InstanceGame)
                            .Where<InstanceGameBook>(x => x.StoreId == request.StoreId);
                            
            var gamesBookVM = gamesBook.ProjectTo<InstanceGameBookViewModel>
                                (mapper.ConfigurationProvider).ToList();

            return new GetAllGameBookResponse() { GamesBook = gamesBookVM};
        }
    }
    public class GetAllGameBookRequest : IRequest<GetAllGameBookResponse>
    {
        public int StoreId { get; set; }
        public int UserId { get; set; }
    }
    public class GetAllGameBookResponse
    {
        public IEnumerable<InstanceGameBookViewModel> GamesBook { get; set; }
    }
}
