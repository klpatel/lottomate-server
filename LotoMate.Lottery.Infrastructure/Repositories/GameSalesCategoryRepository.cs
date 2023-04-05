using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Framework;
using System.Linq;


namespace LotoMate.Lottery.Infrastructure.Repositories
{
    public class GameSalesCategoryRepository : Repository<GameSalesCategory, LotteryDbContext>, IGameSalesCategoryRepository
    {
        public GameSalesCategoryRepository(LotteryDbContext context) : base(context)
        {
        }
        public override void Update(GameSalesCategory gameSalesCategory)
        {
            var context = (LotteryDbContext)Context;
            var existingSale = context.CategorisedSales
                        .Where(m => m.Id == gameSalesCategory.Id).SingleOrDefault();
            Context.Entry(existingSale).CurrentValues.SetValues(gameSalesCategory);
        }
    }
}
