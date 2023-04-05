using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Framework;
using System.Linq;


namespace LotoMate.Lottery.Infrastructure.Repositories
{
    public interface IStoreRepository : IRepository<Store, LotteryDbContext>
    {
    }
    public class StoreRepository : Repository<Store, LotteryDbContext>, IStoreRepository
    {
        public StoreRepository(LotteryDbContext context) : base(context)
        {
        }
        
    }
}
