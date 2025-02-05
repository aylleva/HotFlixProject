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


            builder.Services.AddPersistenceServices(builder.Configuration);


            builder.Services.AddAuthentication(
                opt =>
                {
                    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                }
                ).AddCookie(opt =>
                {
                    opt.LoginPath = "/account/google-login";
                }).AddGoogle(opt =>
            {
                opt.ClientId = builder.Configuration["GoogleKeys:ClientId"];
                opt.ClientSecret = builder.Configuration["GoogleKeys:ClientSecret"];
            }
            );
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
