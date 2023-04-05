using AutoMapper;
using LotoMate.Identity.Infrastructure.Models;
using LotoMate.Identity.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Identity.API.Handlers
{
    public class AddClientRoleHandler : IRequestHandler<AddClientRoleRequest, AddClientRoleResponse>
    {
        private readonly ILogger<AddClientRoleHandler> logger;
        private readonly IMapper mapper;
        private readonly IUserClientRoleRepository userClientRoleRepository ;

        public AddClientRoleHandler(ILogger<AddClientRoleHandler> logger, IMapper mapper, IUserClientRoleRepository userClientRoleRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userClientRoleRepository = userClientRoleRepository;
        }

        public async Task<AddClientRoleResponse> Handle(AddClientRoleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var role = userClientRoleRepository.Queryable().Where(r => r.ClientId == request.Role.ClientId
                            && r.UserId == request.Role.UserId).FirstOrDefault();
                if (role != null)
                    return new AddClientRoleResponse() { Exists = true, Success = true, UserRoleId = role.Id };

                role = mapper.Map<UserClientRole>(request.Role);
                role.AuditAdd(request.UserId);
                userClientRoleRepository.Insert(role);

                return new AddClientRoleResponse() { Exists = false, Success = true};
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error while adding UserClientRole!, userid :  {UserId}",request.UserId);
                throw;
            }
        }

    }

    public class AddClientRoleRequest : IRequest<AddClientRoleResponse>
    {
        public UserClientRoleViewModel Role { get; set; }
        public int? UserId { get; set; }
    }

    public class AddClientRoleResponse
    {
        public bool Exists { get; set; }
        public bool Success { get; set; }
        public int UserRoleId { get; set; }
    }
}
