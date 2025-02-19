using HotFlix.Application.Abstraction.Services;
using HotFlix.Domain.Models;
using HotFlix.Infrastructure.ServiceRegistration;
using HotFlix.Persistence.DAL;
using HotFlix.Persistence.Implementations.Services;
using HotFlix.Persistence.ServiceRegistration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Stripe;
using HotFlix.MiddleWares;

namespace HotFlix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            builder.Services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources";
            });
            builder.Services.Configure<RequestLocalizationOptions>(opt =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("tr-TR")
                };
                opt.DefaultRequestCulture = new RequestCulture("en-US");
                opt.SupportedCultures = supportedCultures;
            });

            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/home/error";
            });

            builder.Services.AddPersistenceServices(builder.Configuration)
                .AddInfrastructureServices(builder.Configuration);

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();
            app.UseRequestLocalization();

            using(var scope = app.Services.CreateScope())
            {
                var initialize=scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
                initialize.InitializeDatabase().Wait();
                initialize.CreateRoles().Wait();
                initialize.CreateAdmin().Wait();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseSession();
            app.UseStaticFiles();
            //app.UseMiddleware<GlobalExceptionHandler>();

            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

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
