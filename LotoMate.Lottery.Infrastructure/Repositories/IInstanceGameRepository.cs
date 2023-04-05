using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Framework;

namespace LotoMate.Lottery.Infrastructure.Repositories
{
    public interface IInstanceGameRepository :  IRepository<InstanceGameMaster, LotteryDbContext>,
        IBulkRepository<InstanceGameMaster, LotteryDbContext>
    {

    }
}
