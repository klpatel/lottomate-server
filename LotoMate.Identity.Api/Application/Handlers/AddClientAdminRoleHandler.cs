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
    public class AddClientAdminRoleHandler : IRequestHandler<AddClientAdminRoleRequest, AddClientAdminRoleResponse>
    {
        private readonly ILogger<AddClientRoleHandler> logger;
        private readonly IMapper mapper;
        private readonly IUserRoleRepository userRoleRepository ;

        public AddClientAdminRoleHandler(ILogger<AddClientRoleHandler> logger, IMapper mapper, IUserRoleRepository userRoleRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<AddClientAdminRoleResponse> Handle(AddClientAdminRoleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var role = userRoleRepository.Queryable().Where(r => r.ClientId == request.Role.ClientId
                            && r.UserId == request.Role.UserId).FirstOrDefault();
                if (role != null)
                    return new AddClientAdminRoleResponse() { Exists = true, Success = true};

                role = mapper.Map<AspNetUserRole>(request.Role);
                userRoleRepository.Insert(role);

                return new AddClientAdminRoleResponse() { Exists = false, Success = true};
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error while adding UserClientRole!, userid :  {UserId}",request.UserId);
                throw;
            }
        }

    }

    public class AddClientAdminRoleRequest : IRequest<AddClientAdminRoleResponse>
    {
        public UserClientRoleViewModel Role { get; set; }
        public int? UserId { get; set; }
    }

    public class AddClientAdminRoleResponse
    {
        public bool Exists { get; set; }
        public bool Success { get; set; }
        
    }
}
