using LotoMate.Client.Infrastructure.Models;
using LotoMate.Framework;
using System.Linq;

namespace LotoMate.Client.Infrastructure.Repositories
{
    public class ClientRepository : Repository<RBAClient, LottoClientDbContext>, IClientRepository
    {
        public ClientRepository(LottoClientDbContext context) : base(context)
        {
        }

        public override void Update(RBAClient rBAClient)
        {
            var context = (LottoClientDbContext)Context;
            var existingBank = context.Clients.Where(m => m.Id == rBAClient.Id).SingleOrDefault();
            Context.Entry(existingBank).CurrentValues.SetValues(rBAClient);
        }
    }
}
