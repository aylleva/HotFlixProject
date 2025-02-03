using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using HotFlix.Persistence.ServiceRegistration;
using Microsoft.AspNetCore.Identity;
using System;

namespace HotFlix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = false;


                opt.User.RequireUniqueEmail = true;

                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.AllowedForNewUsers = true;
            }
          ).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllerRoute(
            "admin",
            "{area:exists}/{controller=home}/{action=index}/{Id?}"
             );

            app.MapControllerRoute(
               "default",
               "{controller=home}/{action=index}/{Id?}"
                );


            app.Run();
        }
    }
}
