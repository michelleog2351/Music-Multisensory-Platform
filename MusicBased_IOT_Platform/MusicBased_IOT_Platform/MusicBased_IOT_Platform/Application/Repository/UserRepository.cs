using Microsoft.EntityFrameworkCore;
using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Data;
using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Repository
{
    /// <summary>
    /// The UserRepository class is responsible for managing user accounts in the database.
    /// </summary>
    /// <param name="db"></param>
    public class UserRepository(AppDBContext db) : IUserRepository
    {
        private readonly AppDBContext _db = db;

        /// <summary>
        /// The AddAsync method is responsible for adding a new user account to the database. 
        /// It takes a UserAccount object as a parameter, adds it to the Users DbSet, 
        /// and then saves the changes to the database asynchronously. 
        /// This allows for efficient handling of database operations without blocking the main thread, 
        /// improving the responsiveness of the application.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AddAsync(UserAccount user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// The GetByUsernameAsync method is responsible for retrieving a user account from the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<UserAccount?> GetByUsernameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// The DeleteAsync method is responsible for deleting a user account from the database 
        /// based on the provided user ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// The GetAllAsync method retrieves all user accounts from the database asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserAccount>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        /// <summary>
        /// The <c>GetByIdAsync</c>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserAccount?> GetByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task UpdateAsync(UserAccount user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
