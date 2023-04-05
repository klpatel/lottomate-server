using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;

namespace LotoMate.Framework
{
    public partial class Repository<TEntity, TDbContext> : IBulkRepository<TEntity, TDbContext>
        where TEntity : class
        where TDbContext : DbContext
    {
        public async Task  BulkDelete(IList<TEntity> bulkEntities)
        {
            await Context.BulkDeleteAsync<TEntity>(bulkEntities);
        }

        public async Task BulkInsert(IList<TEntity> bulkEntities)
        {
            await Context.BulkInsertAsync<TEntity>(bulkEntities);
        }

        public async Task BulkUpdate(IList<TEntity> bulkEntities)
        {
            await Context.BulkUpdateAsync<TEntity>(bulkEntities);
        }
    }
}