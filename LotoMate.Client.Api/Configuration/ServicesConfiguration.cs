using LotoMate.Client.Infrastructure;
using LotoMate.Client.Infrastructure.Repositories;
using LotoMate.Framework;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LotoMate.Client.Api.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LottomateConnection");
            services.AddDbContext<LottoClientDbContext>(x => x.UseSqlServer(connectionString));

            //Inject UnitOfWork for an context
            services.AddScoped<IUnitOfWork<LottoClientDbContext>, UnitOfWork<LottoClientDbContext>>();

            //Continuous update :Inject all services and repositories here
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IBusinessTypeRepository, BusinessTypeRepository>();
            services.AddScoped<IBusinessCategoryRepository, BusinessCategoryRepository>();
            
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ServicesConfiguration).GetTypeInfo().Assembly));

            return services;

            
        }
    }
}
