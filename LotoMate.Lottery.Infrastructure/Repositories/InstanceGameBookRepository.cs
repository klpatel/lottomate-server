using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Framework;
using System.Linq;

namespace LotoMate.Lottery.Infrastructure.Repositories
{
    public class InstanceGameBookRepository : Repository<InstanceGameBook, LotteryDbContext>, IInstanceGameBookRepository
    {
        public InstanceGameBookRepository(LotteryDbContext context) : base(context)
        {
        }
        public override void Update(InstanceGameBook instanceGameBook)
        {
            var context = (LotteryDbContext)Context;
            var existingGameBook = context.InstanceGameBooks
                        .Where(m => m.Id == instanceGameBook.Id).SingleOrDefault();
            Context.Entry(existingGameBook).CurrentValues.SetValues(instanceGameBook);
        }
    }
}
