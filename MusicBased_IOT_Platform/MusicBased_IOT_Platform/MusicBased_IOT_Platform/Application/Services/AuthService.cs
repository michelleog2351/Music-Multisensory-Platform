namespace MusicBased_IOT_Platform.Application.Services
{
    public static class AuthService
    {
        public static bool IsTokenStillValid(DateTime? expiry)
        {
            return expiry != null && expiry > DateTime.UtcNow;
        }
    }
}
