using LotoMate.Client.Infrastructure.Models;
using LotoMate.Framework;
using System.Linq;

namespace LotoMate.Client.Infrastructure.Repositories
{
    public class BusinessCategoryRepository : Repository<BusinessCategory, LottoClientDbContext>, IBusinessCategoryRepository
    {
        public BusinessCategoryRepository(LottoClientDbContext context) : base(context)
        {
        }

    }
}
