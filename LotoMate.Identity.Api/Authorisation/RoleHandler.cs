using LotoMate.Identity.Infrastructure.Models;
using LotoMate.Identity.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotoMate.Identity.API.Authorisation
{
    public class RoleHandler : AuthorizationHandler<HasRoleRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserRoleRepository userRoleRepository;
        private readonly ILogger<RoleHandler> logger;
        private readonly IEnumerable<AspNetRole> roles;
        public RoleHandler(IHttpContextAccessor httpContextAccessor, IUserRoleRepository userRoleRepository, ILogger<RoleHandler> logger)
        {
            this.userRoleRepository = userRoleRepository;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasRoleRequirement requirement)
        {
            try
            {
                var authorizationFilterContext = context.Resource as AuthorizationFilterContext;
                //Authentication check first, return if not authenticated
                if (context?.User?.Identity?.IsAuthenticated == false)
                {
                    return Task.CompletedTask;
                }
                //Check authorization
                var userId = Convert.ToInt32(context?.User.FindFirst(x => x.Type == "userId")?.Value);
                var roles = userRoleRepository.Queryable().Include(x => x.Role)
                                .Where(x => x.UserId == userId).ToList();
                if (roles != null && roles.Any(x => requirement.Roles.Contains(x.Role.Name)))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
                this.logger.LogError("Failed to authorize user. User is not authorised to the requested resources.");
                context?.Fail();
                ResponseAuthError(false);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error while authorizing user.");
                ResponseAuthError(true);
                context.Fail();
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// Method to prepare http response and write
        /// </summary>
        /// <param name="isAuthenticationError"></param>
        private void ResponseAuthError(bool isAuthenticationError)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (!httpContext.Response.HasStarted)
            {
                if (isAuthenticationError)
                    httpContext.Response.StatusCode = 401;
                else
                    httpContext.Response.StatusCode = 403;
            }
        }
    }
}
