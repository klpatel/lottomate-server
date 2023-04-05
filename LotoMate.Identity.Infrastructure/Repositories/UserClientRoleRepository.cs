using LotoMate.Framework;
using LotoMate.Identity.Infrastructure.Models;
using System.Linq;

namespace LotoMate.Identity.Infrastructure.Repositories
{
    public class UserClientRoleRepository : Repository<UserClientRole, IdentityContext>, IUserClientRoleRepository
    {
        public UserClientRoleRepository(IdentityContext context) : base(context)
        {
        }
        
    }
}
