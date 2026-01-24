

namespace MusicBased_IOT_Platform.Services
{
    public class ISpotifyService : SpotifyService
    {
        public ISpotifyService(HttpClient http) : base(http)
        {
        }

        /// <summary>
        internal bool TestDataConnection()
        {
            throw new NotImplementedException();
        }
    }
}
