using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Services
{
    public class UserSessionService
    {
        /// <summary>
        /// The CurrentUser property holds the information about the currently logged-in user.
        /// </summary>
        public UserAccount? CurrentUser { get; private set; }

        /// <summary>
        /// The SetUser method is responsible for setting the current user session by assigning the provided UserAccount object to the CurrentUser property.
        /// </summary>
        /// <param name="user"></param>
        public void SetUser(UserAccount user)
        {
            CurrentUser = user;
        }

        /// <summary>
        /// The Logout method clears the current user session by setting the CurrentUser property to null. 
        /// This effectively logs out the user from the application, as there will be no active user session after this method is called.
        /// </summary>
        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
