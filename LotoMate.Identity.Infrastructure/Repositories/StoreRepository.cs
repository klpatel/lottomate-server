using LotoMate.Framework;
using LotoMate.Identity.Infrastructure.Models;

namespace LotoMate.Identity.Infrastructure.Repositories
{
    public interface IStoreRepository : IRepository<Store, IdentityContext>
    {
    }
    public class StoreRepository : Repository<Store , IdentityContext>, IStoreRepository
    {
        public StoreRepository(IdentityContext context) : base(context)
        {
        }
    }
}

