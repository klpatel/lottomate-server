using LotoMate.Framework;
using LotoMate.Identity.Infrastructure.Models;
using System.Linq;

namespace LotoMate.Identity.Infrastructure.Repositories
{
    public class RoleRepository : Repository<AspNetRole, IdentityContext>, IRoleRepository
    {
        public RoleRepository(IdentityContext context) : base(context)
        {
        }
        
    }
}
