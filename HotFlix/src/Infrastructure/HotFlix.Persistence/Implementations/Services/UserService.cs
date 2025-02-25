using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Persistence.Implementations.Services
{
    internal class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _usermeneger;

        public UserService(AppDbContext context,UserManager<AppUser> usermeneger)
        {
             _context = context;
            _usermeneger = usermeneger;
        }
       
        public async Task<IEnumerable<UsersDto>> GetAllAsync(int page, int take,string? search)
        {
            IEnumerable<AppUser> users = await _usermeneger.Users.
                Include(u=>u.PremiumPlan).Include(u=>u.Comments).Include(u=>u.Reviews).Skip((page - 1) * take).Take(take)
                .ToListAsync();
            if (search is not null)
            {
                users = users.Where(u => u.UserName.Contains(search));
            }
            IEnumerable<UsersDto> usersdto = users.Select(u => new UsersDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Status = u.IsBanned,
                CommentCount = u.Comments.Count(),
                ReviewCount = u.Reviews.Count(),
                Image = u.Image,
                PremiumPlan = u.PremiumPlan.PlanName,

            });
            return usersdto;
        }
        public async Task DeleteAsync(string id)
        {
          var user=await _usermeneger.Users.FirstOrDefaultAsync(u=>u.Id==id);
          if (user is null) throw new Exception("User Was Not Found");

          await _usermeneger.DeleteAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<int> CountAsync()
        {
            return await _usermeneger.Users.CountAsync();
        }

        public async Task ChangeStatus(string Id)
        {
            var user = await _usermeneger.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if (user is null) throw new Exception("User Was Not Found");

            if (user.IsBanned == false)
            {
                user.IsBanned = true;
               
            }
            else
            {
                user.IsBanned = false;
            }

            await _usermeneger.UpdateAsync(user);
        }
    }
}
