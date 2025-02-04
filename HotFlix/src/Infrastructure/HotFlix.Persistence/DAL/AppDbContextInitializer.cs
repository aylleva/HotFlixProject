using HotFlix.Domain.Enums;
using HotFlix.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Persistence.DAL
{
     public class AppDbContextInitializer
    {
        private readonly UserManager<AppUser> usermeneger;
        private readonly AppDbContext context;
        private readonly RoleManager<IdentityRole> userRole;
        private readonly IConfiguration configuration;

        public AppDbContextInitializer(UserManager<AppUser> usermeneger
            ,AppDbContext context
            ,RoleManager<IdentityRole> userRole,
            IConfiguration configuration
            )
        {
            this.usermeneger = usermeneger;
            this.context = context;
            this.userRole = userRole;
            this.configuration = configuration;
        }

        public async Task InitializeDatabase()
        {
            await context.Database.MigrateAsync();
        }
        public async Task CreateRoles()
        {
            foreach(var role in Enum.GetValues(typeof(UserRoles)))
            {
                if(! await userRole.RoleExistsAsync(role.ToString()))
                {
                    await userRole.CreateAsync(new IdentityRole { Name = role.ToString() });    
                }
            }
        }

        public async Task CreateAdmin()
        {
            AppUser admin = new AppUser()
            {
                Name = "admin",
                Surname = "admin",
                Email = configuration["Admin:Email"],
                UserName = configuration["Admin:UserName"]
            };

            await usermeneger.CreateAsync(admin, configuration["Admin:Password"]);
            await usermeneger.AddToRoleAsync(admin,UserRoles.Admin.ToString());
        }
    }
}
