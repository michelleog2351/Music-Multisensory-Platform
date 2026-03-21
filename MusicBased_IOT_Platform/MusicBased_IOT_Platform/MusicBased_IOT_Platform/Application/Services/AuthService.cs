namespace MusicBased_IOT_Platform.Application.Services
{
    public class AuthService
    {
        public bool IsTokenStillValid(DateTime? expiry)
        {
            return expiry != null && expiry > DateTime.UtcNow;
        }
    }
}
