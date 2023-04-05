using LotoMate.Framework;
using LotoMate.Identity.Infrastructure.Models;
using System.Linq;

namespace LotoMate.Identity.Infrastructure.Repositories
{
    public class UserRoleRepository : Repository<AspNetUserRole, IdentityContext>, IUserRoleRepository
    {
        public UserRoleRepository(IdentityContext context) : base(context)
        {
        }
        
    }
}
