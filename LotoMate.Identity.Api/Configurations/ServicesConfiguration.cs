using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LotoMate.Identity.Infrastructure;
using LotoMate.Identity.Infrastructure.Services;
using System.Reflection;
using MediatR;
using LotoMate.Identity.Infrastructure.Repositories;
using LotoMate.Framework;

namespace LotoMate.Identity.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LottomateConnection");
            services.AddDbContext<IdentityContext>(x => x.UseSqlServer(connectionString));

            //Inject UnitOfWork for an context
            services.AddScoped<IUnitOfWork<IdentityContext>, UnitOfWork<IdentityContext>>();

            //Identity services and repositories here
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IAspnetUserRepository, AspnetUserRepository>();

            //Client Service
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUserClientRoleRepository, UserClientRoleRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ServicesConfiguration).GetTypeInfo().Assembly));

            return services;

        }
    }
}
