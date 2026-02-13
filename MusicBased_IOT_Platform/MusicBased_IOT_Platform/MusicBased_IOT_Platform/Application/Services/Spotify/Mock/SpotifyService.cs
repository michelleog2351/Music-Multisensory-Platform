using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Services.Spotify.Mock
{
    public class SpotifyService
    {
        private readonly MockSpotifyDataService _dataService;

        public SpotifyService(MockSpotifyDataService dataService)
        {
            _dataService = dataService;
        }

        public NewReleases GetNewReleases()
        {
            return _dataService.GetNewAlbumReleases();
        }
    }


}


