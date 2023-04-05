using AutoMapper;
using LotoMate.Client.Api.ViewModels;
using LotoMate.Client.Infrastructure.Models;
using LotoMate.Client.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Client.Api.Handlers
{
    public class GetClientHandler : IRequestHandler<GetClientRequest, GetClientResponse>
    {
        private readonly ILogger<GetClientHandler> logger;
        private readonly IMapper mapper;
        private readonly IClientRepository clientRepository;

        public GetClientHandler(ILogger<GetClientHandler> logger, IMapper mapper, IClientRepository clientRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.clientRepository = clientRepository;
        }

        public async Task<GetClientResponse> Handle(GetClientRequest request, CancellationToken cancellationToken)
        {
            
            var client = clientRepository.Queryable().Where<RBAClient>(x=>x.Id == request.Id).FirstOrDefault();
            var clientVM = mapper.Map<RBAClientViewModel>(client);
            return new GetClientResponse() { RBAClient = clientVM };
        }

    }

    public class GetClientRequest : IRequest<GetClientResponse>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }

    public class GetClientResponse
    {
      public  RBAClientViewModel RBAClient { get; set; } 
    }
}
