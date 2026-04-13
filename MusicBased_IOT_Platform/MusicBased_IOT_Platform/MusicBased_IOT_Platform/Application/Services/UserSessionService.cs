using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Services
{
    public class UserSessionService(ProtectedLocalStorage storage)
    {
        private readonly ProtectedLocalStorage _storage = storage;

        /// <summary>
        /// The CurrentUser property holds the information about the currently logged-in user.
        /// </summary>
        public UserAccount? CurrentUser { get; private set; }

        /// <summary>
        /// The OnChange event is an Action delegate that can be subscribed to by other components or services to be notified when the user session state changes.
        /// </summary>
        public event Action? OnChange;

        /// <summary>
        /// The NotifyStateChanged method is responsible for invoking the OnChange event, which notifies any subscribers that the state of the user session has changed.
        /// </summary>
        private void NotifyStateChanged() => OnChange?.Invoke();

        /// <summary>
        /// The SetUser method is responsible for setting the current user session by assigning the provided UserAccount object to the CurrentUser property.
        /// </summary>
        /// <param name="user"></param>
        public async Task SetUserAsync(UserAccount user)
        {
            CurrentUser = user;

            Console.WriteLine("SESSION SET: " + user.FirstName);

            await _storage.SetAsync("userSession", user);

            NotifyStateChanged();
        }

        /// <summary>
        /// The LoadUserAsync method is responsible for loading the user session from the local storage. 
        /// It retrieves the user session data using the GetAsync method of the ProtectedLocalStorage class and assigns the retrieved UserAccount object 
        /// to the CurrentUser property if the retrieval is successful.
        /// </summary>
        /// <returns></returns>
        public async Task LoadUserAsync()
        {
            try
            {
                var result = await _storage.GetAsync<UserAccount>("userSession");

                if (result.Success && result.Value != null)
                {
                    CurrentUser = result.Value;

                    Console.WriteLine("SESSION RESTORED: " + CurrentUser.FirstName);
                }

                else
                {
                    Console.WriteLine("SESSION FOUND");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("SESSION ERROR: " + ex.Message);
                CurrentUser = null;
            }

            NotifyStateChanged();
        }

        /// <summary>
        /// The LogoutAsync method is responsible for logging out the user by clearing the CurrentUser property and removing the user session data from the local storage.
        /// </summary>
        /// <returns></returns>
        public async Task LogoutAsync()
        {
            CurrentUser = null;

            NotifyStateChanged();
            await _storage.DeleteAsync("userSession");
        }
    }
}
