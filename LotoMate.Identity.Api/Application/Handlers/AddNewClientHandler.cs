using AutoMapper;
using LotoMate.Identity.API.ViewModels;
using LotoMate.Identity.Infrastructure.Models;
using LotoMate.Identity.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Identity.API.Handlers
{
    public class AddNewClientHandler : IRequestHandler<AddNewClientRequest, AddNewClientResponse>
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
            try
            {
                //Add new client, Return existing if it finds
                var client = clientRepository.Queryable().Include(c => c.Contact)
                                .Where(x => x.ClientFname == request.Client.ClientFname && x.ClientLname == request.Client.ClientLname
                                        && x.Contact.Email1 == request.Client.EmailId)
                                .FirstOrDefault();
                if (client != null)
                {
                    logger.LogInformation("Client already exists with Id : {ClientId} ", client.Id);
                    return new AddNewClientResponse() { ClientId = client.Id, Exists = true, Suceess = true };
                }
                client = mapper.Map<RBAClient>(request.Client);
                client.AuditAdd(request.UserId);
                client = await clientRepository.AddAndSave(client);
                logger.LogInformation("New Client is added with Id : {ClientId} ", client.Id);

                //return result
                return new AddNewClientResponse() { ClientId = client.Id,Exists=false, Suceess = true };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while adding Client!, userid :  {UserId}", request.UserId);
                throw;
            }
        }

    }

    public class AddNewClientRequest : IRequest<AddNewClientResponse>
    {
        public RBAClientViewModel Client { get; set; }
        public int? UserId { get; set; }
    }

    public class AddNewClientResponse
    {
        public bool Exists { get; set; }
        public bool Suceess { get; set; }
        public int ClientId { get; set; }
    }
}
