using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LotoMate.Framework
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext>
    where TDbContext : DbContext
    {
        private readonly IMediator mediator;

        protected TDbContext Context { get; }

        public UnitOfWork(TDbContext context, IMediator mediator)
        {
            Context = context;
            this.mediator = mediator;
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await mediator.DispatchDomainEventsAsync(Context);

                return await Context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        //public virtual async Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        //    => await Context.Database.CommitTransaction.ExecuteSqlCommandAsync(sql, parameters, cancellationToken);
    }
}