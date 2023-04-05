using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Framework;
using System.Linq;

namespace LotoMate.Lottery.Infrastructure.Repositories
{
    public interface ICategorySalesRepository : IRepository<CategorisedSale, LotteryDbContext>,
                IBulkRepository<CategorisedSale, LotteryDbContext>
    {

    }

    public class CategorySalesRepository : Repository<CategorisedSale, LotteryDbContext>, ICategorySalesRepository
    {
        public CategorySalesRepository(LotteryDbContext context) : base(context)
        {
        }
        public override void Update(CategorisedSale categorisedSale)
        {
            var context = (LotteryDbContext)Context;
            var existingSale = context.CategorisedSales
                        .Where(m => m.Id == categorisedSale.Id).SingleOrDefault();
            Context.Entry(existingSale).CurrentValues.SetValues(categorisedSale);
        }
    }
}
