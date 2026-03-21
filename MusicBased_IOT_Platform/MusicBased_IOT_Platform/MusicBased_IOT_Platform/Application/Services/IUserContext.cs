using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Services
{
    public interface IUserContext
    {
        Task<UserAccount?> GetCurrentUserAsync();
    }
}
