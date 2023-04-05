using AutoMapper;
using LotoMate.Exceptions;
using LotoMate.Identity.API.ViewModels;
using LotoMate.Identity.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.API.Application.Command
{
    public class AddUserHandler : IRequestHandler<AddUserRequest, AddUserResponse>
    {
        private readonly ILogger<AddUserHandler> logger;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public AddUserHandler(ILogger<AddUserHandler> logger, IMapper mapper,
                                          UserManager<User> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<AddUserResponse> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            var findUser = await userManager.FindByEmailAsync(request.User.Email);
            if (findUser == null)
                findUser = request.User.UserName != null ? await userManager.FindByNameAsync(request.User.UserName) : null;

            if (findUser != null)
            {
                logger.LogInformation("User already exists with Id : {UserId} ", findUser.Id);
                return new AddUserResponse() { Exists = true, Suceess = true, UserId = findUser.Id };
            }
            var user = mapper.Map<User>(request.User);
            var result = await userManager.CreateAsync(user, request.User.Password);
            
            if (!result.Succeeded)
                throw new IdentityException(JsonConvert.SerializeObject(result.Errors));

            logger.LogInformation("User added with Id : {UserId} ", user.Id);
            return new AddUserResponse() { Exists = false, Suceess = true, UserId = user.Id };
        }
    }
    public class AddUserRequest : IRequest<AddUserResponse>
    {
        public UserViewModel User { get; set; }

    }
    public class AddUserResponse
    {
        public bool Exists { get; set; }
        public bool Suceess { get; set; }
        public int UserId { get; set; }
    }
}
