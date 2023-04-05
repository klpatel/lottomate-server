using LotoMate.Client.Infrastructure.Models;
using LotoMate.Framework;
using System.Linq;

namespace LotoMate.Client.Infrastructure.Repositories
{
    public class BusinessTypeRepository : Repository<BusinessType, LottoClientDbContext>, IBusinessTypeRepository
    {
        public BusinessTypeRepository(LottoClientDbContext context) : base(context)
        {
        }

    }
}
