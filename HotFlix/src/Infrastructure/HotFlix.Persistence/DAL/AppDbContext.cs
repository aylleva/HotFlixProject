using HotFlix.Domain;
using HotFlix.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Persistence.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<MovieActors> MoviesActors { get; set; }
        public DbSet<MovieTags> MovieTags { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<PremiumPlan> PremiumPlans { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SeasonVideos> SeasonVideos { get; set; }
        public DbSet<PartnerShip> PartnerShips { get; set; }
        public DbSet<JobContact> JobContacts {  get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<PlanOrder> PlanOrders { get; set; }
        public DbSet<FavoriteMovies> FavoriteMovies { get; set; }
        public DbSet<MovieWatches> MovieWatches { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
