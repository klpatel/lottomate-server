using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LotoMate.Framework
{
    public interface IUnitOfWork<TDbContext>
       where TDbContext : DbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        //Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default );
    }
}