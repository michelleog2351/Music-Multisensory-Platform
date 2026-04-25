using System.ComponentModel.DataAnnotations;

namespace MusicBased_IOT_Platform.Models
{
    /// <summary>
    /// The UserAccount class represents a user account in the system, containing properties for user information 
    /// and authentication details.
    /// </summary>
    public class UserAccount
    {
        public int ID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? FitbitAccessToken { get; set; }
        public string? FitbitRefreshToken { get; set; }
        public DateTime? FitbitTokenExpiry { get; set; }
        public string? SpotifyAccessToken { get; set; }
        public string? SpotifyRefreshToken { get; set; }
        public DateTime? SpotifyTokenExpiry { get; set; }
    }
}

