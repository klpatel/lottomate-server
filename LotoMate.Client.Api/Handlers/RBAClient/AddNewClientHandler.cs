using AutoMapper;
using LotoMate.Client.Api.ViewModels;
using LotoMate.Client.Infrastructure.Models;
using LotoMate.Client.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Client.Api.Handlers
{
    public class AddNewClientHandler : IRequestHandler<AddNewClientRequest,AddNewClientResponse>
    {
        private readonly ILogger<AddNewClientHandler> logger;
        private readonly IMapper mapper;
        private readonly IClientRepository clientRepository;

        public AddNewClientHandler(ILogger<AddNewClientHandler> logger, IMapper mapper, IClientRepository clientRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.clientRepository = clientRepository;
        }

        public async Task<AddNewClientResponse> Handle(AddNewClientRequest request, CancellationToken cancellationToken)
        {
            var client = mapper.Map<RBAClient>(request.Client);
            clientRepository.Insert(client);

            return new AddNewClientResponse();
        }

    }

    public class AddNewClientRequest : IRequest<AddNewClientResponse>
    {
        public RBAClientViewModel Client { get; set; }
        public int UserId { get; set; }
    }

    public class AddNewClientResponse
    {

    }
}
