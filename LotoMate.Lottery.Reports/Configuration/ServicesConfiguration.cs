using DinkToPdf;
using DinkToPdf.Contracts;
using LotoMate.Framework;
using LotoMate.Lottery.Infrastructure;
using LotoMate.Lottery.Infrastructure.Repositories;
using LotoMate.Reports.Library.PdfProcessor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LotoMate.Lottery.Reports.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddReportServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LottomateConnection");
            services.AddDbContext<LotteryDbContext>(x => x.UseSqlServer(connectionString));

            //Inject UnitOfWork for an context
            services.AddScoped<IUnitOfWork<LotteryDbContext>, UnitOfWork<LotteryDbContext>>();

            //Continuous update :Inject all services and repositories here
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddScoped<IPdfProcessor, DinkPdfProcessor>();

            services.AddScoped<IInstanceGameSalesRepository, InstanceGameSalesRepository>();
            services.AddScoped<ICategorySalesRepository, CategorySalesRepository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ServicesConfiguration).GetTypeInfo().Assembly));

            return services;

            
        }
    }
}
