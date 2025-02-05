

namespace HotFlix.Application.Abstraction.Services
{
    public interface ILayoutService
    {
        Task<Dictionary<string, string>> GetSettings();
    }
}
