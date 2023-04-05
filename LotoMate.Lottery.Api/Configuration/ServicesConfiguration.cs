using LotoMate.Framework;
using LotoMate.Lottery.Infrastructure;
using LotoMate.Lottery.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LotoMate.Lottery.Api.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddLottoServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LottomateConnection");
            services.AddDbContext<LotteryDbContext>(x => x.UseSqlServer(connectionString));

            //Inject UnitOfWork for an context
            services.AddScoped<IUnitOfWork<LotteryDbContext>, UnitOfWork<LotteryDbContext>>();

            //Continuous update :Inject all services and repositories here
            services.AddScoped<IInstanceGameRepository, InstanceGameRepository>();
            services.AddScoped<IInstanceGameBookRepository, InstanceGameBookRepository>();
            services.AddScoped<IInstanceGameSalesRepository, InstanceGameSalesRepository>();
            services.AddScoped<ICategorySalesRepository, CategorySalesRepository>();
            services.AddScoped<IGameSalesCategoryRepository, GameSalesCategoryRepository>();

            services.AddMediatR(typeof(ServicesConfiguration).GetTypeInfo().Assembly);

            return services;

            
        }
    }
}
