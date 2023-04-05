using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Identity.Infrastructure.Models
{
    public class Role : IdentityRole<int>
    {
        public Role() : base() { }

        public Role(string roleName) : base(roleName) { }

        
    }
}
