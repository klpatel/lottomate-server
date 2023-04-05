using AutoMapper;
using LotoMate.Client.Api.ViewModels;
using LotoMate.Client.Infrastructure.Models;
using LotoMate.Client.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Client.Api.Handlers
{
   public class UpdateClientHandler : IRequestHandler<UpdateClientRequest, UpdateClientResponse>
    {
        private readonly ILogger<UpdateClientHandler> logger;
        private readonly IMapper mapper;
        private readonly IClientRepository clientRepository;

        public UpdateClientHandler(ILogger<UpdateClientHandler> logger, IMapper mapper, IClientRepository clientRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.clientRepository = clientRepository;
        }

        public async Task<UpdateClientResponse> Handle(UpdateClientRequest request, CancellationToken cancellationToken)
        {
            var client = mapper.Map<RBAClient>(request.Client);
            clientRepository.Update(client);

            return new UpdateClientResponse();
        }

    }

    public class UpdateClientRequest : IRequest<UpdateClientResponse>
    {
        public RBAClientViewModel Client { get; set; }
        public int UserId { get; set; }
    }

    public class UpdateClientResponse
    {

    }
}
