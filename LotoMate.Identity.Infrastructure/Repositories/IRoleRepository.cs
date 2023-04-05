using LotoMate.Identity.Infrastructure.Models;
using LotoMate.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using LotoMate.Identity.Infrastructure;

namespace LotoMate.Identity.Infrastructure.Repositories
{
    public interface IRoleRepository : IRepository<AspNetRole, IdentityContext>
    {
    }
}
