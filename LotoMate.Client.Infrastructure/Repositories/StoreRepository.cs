using LotoMate.Client.Infrastructure.Models;
using LotoMate.Framework;
using System.Linq;

namespace LotoMate.Client.Infrastructure.Repositories
{
    public class StoreRepository : Repository<Store, LottoClientDbContext>, IStoreRepository
    {
        public StoreRepository(LottoClientDbContext context) : base(context)
        {
        }

        public override void Update(Store store)
        {
            var context = (LottoClientDbContext)Context;
            var existingBank = context.Stores.Where(m => m.Id == store.Id).SingleOrDefault();
            Context.Entry(existingBank).CurrentValues.SetValues(store);
        }
    }
}
