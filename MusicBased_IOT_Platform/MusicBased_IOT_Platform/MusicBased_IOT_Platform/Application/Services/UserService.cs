using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Services
{
    /// <summary>
    /// The UserService class is responsible for managing user accounts in the application.
    /// </summary>
    /// <param name="userRepo"></param>
    public class UserService(IUserRepository userRepo) : IUserService
    {
        /// <summary>
        /// The IUserRepository is injected into the UserService class through the constructor.
        /// </summary>
        private readonly IUserRepository _userRepo = userRepo;

        /// <summary>
        /// The RegisterUserAsync method is responsible for registering a new user account in the application.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool Success, string? Error)> RegisterUserAsync(RegisterModel model)
        {
            // Check if the username already exists
            var existingUser = await _userRepo.GetByUsernameAsync(model.Username);
            if (existingUser != null)
            {
                return (false, "Username already exists.");
            }

            // Create a new user account
            var newUser = new UserAccount
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Username = model.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            // Add the new user to the database
            await _userRepo.AddAsync(newUser);
            return (true, "User registered successfully.");
        }

        /// <summary>
        /// The LoginUserAsync method is responsible for logging in a user to the application.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(bool Success, string? Error, UserAccount? User)> LoginUserAsync(LoginModel model)
        {
            var user = await _userRepo.GetByUsernameAsync(model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return (false, "Invalid username or password.", null);
            }

            return (true, null, user);
        }

        public Task LogoutUserAsync()
        {
            return Task.CompletedTask;
        }

        public Task<UserAccount?> GetCurrentUserAsync()
        {
            return Task.FromResult<UserAccount?>(null);
        }
    }
}
