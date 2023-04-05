using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using LotoMate.Identity.Infrastructure;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using LotoMate.Identity.Infrastructure.Models;
using LotoMate.Identity.API.Extensions;

namespace LotoMate.Identity.API.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LotoMateConnection");
            services.AddDbContext<IdentityContext>(x => x.UseSqlServer(connectionString));

            var identity = services.AddIdentityCore<User>(opts => IdentityPolicy.BuildPasswordOptions());
            
            IdentityBuilder builder = new IdentityBuilder(identity.UserType, typeof(Role), identity.Services);
            builder.AddSignInManager<SignInManager<User>>();
            builder.AddEntityFrameworkStores<IdentityContext>();
            builder.AddDefaultTokenProviders();
            builder.AddTokenProvider("LotoMate", typeof(DataProtectorTokenProvider<>).MakeGenericType(typeof(User)));
            builder.AddRoles<Role>();

            //remove default claims
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Security:Tokens:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Security:Tokens:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Security:Tokens:Key"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            return services;


        }
    }
}
