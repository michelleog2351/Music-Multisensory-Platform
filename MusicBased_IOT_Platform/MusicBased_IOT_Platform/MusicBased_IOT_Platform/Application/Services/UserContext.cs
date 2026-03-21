using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Services
{
    public class UserContext(UserSessionService session) : IUserContext
    {
        private readonly UserSessionService _session = session;

        /// <summary>
        /// The <c>GetCurrentUserAsync</c>
        /// </summary>
        /// <returns></returns>
        public async Task<UserAccount?> GetCurrentUserAsync()
        {
            await _session.LoadUserAsync();
            return _session.CurrentUser;
        }
    }
}
