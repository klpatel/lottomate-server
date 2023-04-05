using Microsoft.EntityFrameworkCore;

namespace LotoMate.Framework
{
    public interface IService<TEntity, TDbContext>   where TEntity : class
          where TDbContext : DbContext
    {
    }
}
