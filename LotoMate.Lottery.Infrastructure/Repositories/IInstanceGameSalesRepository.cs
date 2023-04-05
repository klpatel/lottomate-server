using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Framework;

namespace LotoMate.Lottery.Infrastructure.Repositories
{
    public interface IInstanceGameSalesRepository : IRepository<InstanceDailySale, LotteryDbContext>,
        IBulkRepository<InstanceDailySale, LotteryDbContext>
    {

    }
}
