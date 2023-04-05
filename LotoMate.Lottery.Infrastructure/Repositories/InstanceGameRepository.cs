using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Framework;
using System.Linq;

namespace LotoMate.Lottery.Infrastructure.Repositories
{
    public class InstanceGameRepository : Repository<InstanceGameMaster, LotteryDbContext>, IInstanceGameRepository
    {
        public InstanceGameRepository(LotteryDbContext context) : base(context)
        {
        }

        public override void Update(InstanceGameMaster instanceGameMaster)
        {
            var context = (LotteryDbContext)Context;
            var existingGame = context.InstanceGameMasters
                        .Where(m => m.Id == instanceGameMaster.Id).SingleOrDefault();
            Context.Entry(existingGame).CurrentValues.SetValues(instanceGameMaster);
        }
    }
}
