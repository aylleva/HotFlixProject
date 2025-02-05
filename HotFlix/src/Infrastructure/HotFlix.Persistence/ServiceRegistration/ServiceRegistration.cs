using Microsoft.AspNetCore.Identity;
using HotFlix.Persistence.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HotFlix.Domain.Models;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Persistence.Implementations.Services;
using System.Reflection;


namespace HotFlix.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("Default"),
             m => m.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
            ));
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = false;


                opt.User.RequireUniqueEmail = true;

                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.AllowedForNewUsers = true;
            }
          ).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddScoped<AppDbContextInitializer>();
            services.AddScoped<ILayoutService,LayoutService>();
            
            return services;
        }
    }
}
