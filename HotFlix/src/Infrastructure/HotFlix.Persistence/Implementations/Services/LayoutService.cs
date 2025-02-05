using HotFlix.Application.Abstraction.Services;
using HotFlix.Persistence.DAL;
using Microsoft.EntityFrameworkCore;


namespace HotFlix.Persistence.Implementations.Services
{
    internal class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string, string>> GetSettings()
        {
            return await _context.Settings.ToDictionaryAsync(s=>s.Key,s=>s.Value);
        }
    }
}
