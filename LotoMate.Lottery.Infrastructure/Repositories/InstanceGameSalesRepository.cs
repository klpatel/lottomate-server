using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Framework;
using System.Linq;

namespace LotoMate.Lottery.Infrastructure.Repositories
{
    public class InstanceGameSalesRepository : Repository<InstanceDailySale, LotteryDbContext>,
        IInstanceGameSalesRepository
    {
        public InstanceGameSalesRepository(LotteryDbContext context) : base(context)
        {
        }
        public override void Update(InstanceDailySale instanceDailySale)
        {
            var context = (LotteryDbContext)Context;
            var existingGameBook = context.InstanceDailySales
                        .Where(m => m.Id == instanceDailySale.Id).SingleOrDefault();
            Context.Entry(existingGameBook).CurrentValues.SetValues(instanceDailySale);
        }
    }
}
