using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Interfaces
{
    public interface IUserService
    {
        Task<(bool Success, string? Error)> RegisterUserAsync(RegisterModel model);

        Task<(bool Success, string? Error)> LoginUserAsync(LoginModel model);
    
        Task LogoutUserAsync();
  
        Task<UserAccount?> GetCurrentUserAsync();
    }
}