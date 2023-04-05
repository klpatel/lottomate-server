using LotoMate.Client.Infrastructure.Models;
using LotoMate.Framework;

namespace LotoMate.Client.Infrastructure.Repositories
{
    public interface IClientRepository : IRepository<RBAClient, LottoClientDbContext>
    {

    }
}
