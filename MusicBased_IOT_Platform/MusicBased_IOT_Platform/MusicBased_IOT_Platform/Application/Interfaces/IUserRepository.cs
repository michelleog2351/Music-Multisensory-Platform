using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(UserAccount user);
        Task<UserAccount?> GetByUsernameAsync(string username);
        Task<IEnumerable<UserAccount>> GetAllAsync();
    }
}
