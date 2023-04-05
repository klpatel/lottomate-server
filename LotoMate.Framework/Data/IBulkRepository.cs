using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LotoMate.Framework
{
    public interface IBulkRepository<TEntity, TDbContext> where TEntity : class
    {
        Task BulkInsert(IList<TEntity> bulkEntities);
        Task BulkUpdate(IList<TEntity> bulkEntities);
        Task BulkDelete(IList<TEntity> bulkEntities);
                
    }
}