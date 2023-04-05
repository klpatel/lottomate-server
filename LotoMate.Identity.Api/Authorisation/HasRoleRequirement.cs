using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Identity.API.Authorisation
{
    public class HasRoleRequirement : IAuthorizationRequirement
    {
        public HasRoleRequirement(string[] roles)
        {
            Roles = roles;
        }

        public string[] Roles { get; }
    }
}
