using LotoMate.Framework;
using LotoMate.Identity.Infrastructure.Models;
using System.Linq;

namespace LotoMate.Identity.Infrastructure.Repositories
{
    public class ClientRepository : Repository<RBAClient, IdentityContext>, IClientRepository
    {
        public ClientRepository(IdentityContext context) : base(context)
        {
        }
        
    }
}
