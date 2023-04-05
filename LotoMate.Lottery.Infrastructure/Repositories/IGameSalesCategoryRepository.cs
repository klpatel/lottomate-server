using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Framework;

namespace LotoMate.Lottery.Infrastructure.Repositories
{
    public interface IGameSalesCategoryRepository : IRepository<GameSalesCategory, LotteryDbContext>
    {
    }
}
