using Microsoft.AspNetCore.Identity;
using HotFlix.Persistence.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HotFlix.Domain.Models;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Persistence.Implementations.Services;
using System.Reflection;
using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Persistence.Implementations.Repositories;


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

            services.AddScoped<ICategoryRepository,CategoryRepository>();   
            services.AddScoped<ICategoryService,CategoryService>(); 

            services.AddScoped<ICountryService,CountryService>();
            services.AddScoped<ICountryRepository,CountryRepository>();

            services.AddScoped<IDirectorRepository, DirectorRepository>();
            services.AddScoped<IDirectorService, DirectorService>();

            services.AddScoped<IPartnerShipRepository, PartnerShipRepository>();
            services.AddScoped<IPartnerShipService, PartnerShipService>();

            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();

            services.AddScoped<IPremiumPlanRepository, PremiumPlanRepository>();
            services.AddScoped<IPremiumService, PremiumService>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewService, ReviewService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();

            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IActorService, ActorService>();


            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();

            return services;
        }
    }
}
