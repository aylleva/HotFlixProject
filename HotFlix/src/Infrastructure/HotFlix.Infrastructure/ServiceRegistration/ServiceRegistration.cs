using HotFlix.Application.Abstraction.Services;
using HotFlix.Infrastructure.Implementations.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace HotFlix.Infrastructure.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie()
            .AddGoogle(GoogleDefaults.AuthenticationScheme, option =>
            {
                option.ClientId = configuration.GetSection("GoogleKeys:ClientId").Value;
                option.ClientSecret = configuration.GetSection("GoogleKeys:ClientSecret").Value;

            });
            services.AddScoped<IEmailService, EmailService>();
            return services;    
        }

    }
}
