using LotoMate.API.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using LotoMate.Identity.API.Configurations;

namespace LotoMate.API
{
    public class Startup
    {

        private readonly ILogger<Startup> logger;
        private readonly string corsPolicyName = "LotoMateCorsPolicy";
        public IConfiguration Configuration { get; }
        public Startup(IWebHostEnvironment environment, ILogger<Startup> logger)
        {
            this.logger = logger;
            var builder = new ConfigurationBuilder()
               .SetBasePath(environment.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string[] corsAllowedUrls = Configuration.GetSection("EnvironmetVariables")["CORS_ALLOWED_URLS"] == null
                ? Configuration.GetSection("Configuration")["CorsAllowedUrls"].Split(',') : Configuration.GetSection("EnvironmetVariables")["CORS_ALLOWED_URLS"].Split(',');

            services.AddCors(x =>
                {
                    x.AddPolicy(corsPolicyName, y =>
                    {
                        y.AllowAnyHeader();
                        y.AllowAnyMethod();
                        y.AllowAnyOrigin();
                    });
                })
                .AddHttpContextAccessor()
                .ConfigureServices(Configuration)
                .ConfigureSwagger()
                .AddIdentity(Configuration)
                .AddPolicies(Configuration)
                .AddMiddleware();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(corsPolicyName);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStatusCodePagesWithReExecute("/errors", "?statusCode={0}");
            app.UseExceptionHandler("/errors");

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("LotoMate.API.Resources.swagger-ui-index.html");
                c.SwaggerEndpoint("/swagger/LotoMate.Identity.API/swagger.json", "Identity Module Api");
                c.SwaggerEndpoint("/swagger/LotoMate.Client.API/swagger.json", "Client Module Api");
                c.SwaggerEndpoint("/swagger/LotoMate.Lottery.API/swagger.json", "Lottery Module Api");
                c.SwaggerEndpoint("/swagger/LotoMate.Lottery.Reports/swagger.json", "Lottery Module Reports");
            });

        }
    }
}
