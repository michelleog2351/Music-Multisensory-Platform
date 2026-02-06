using MusicBased_IOT_Platform.Models;
using MusicBased_IOT_Platform.Services;

namespace MusicBased_IOT_Platform.Application.Interfaces
{
    public class ISpotifyService : SpotifyService
    {
        public ISpotifyService(HttpClient http) : base(http)
        {
        }

        internal Album GetAlbum(string id)
        {
            throw new NotImplementedException();
        }

        internal Artist GetArtist(string id)
        {
            throw new NotImplementedException();
        }

        internal Recommendations GetMoodRecommendations(int limit, double max_danceability, double max_energy, double max_valence, double max_liveness)
        {
            throw new NotImplementedException();
        }

        internal NewReleases GetNewAlbumReleases()
        {
            throw new NotImplementedException();
        }

        internal Recommendations GetRecommendations(string seedArtists, string seedGenres, string seedTracks, int limit, string market)
        {
            throw new NotImplementedException();
        }

        internal Track GetTrack(string id)
        {
            throw new NotImplementedException();
        }

        internal SearchResults Search(string searchQuery, string searchItemTypes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        internal bool TestDataConnection()
        {
            throw new NotImplementedException();
        }
    }
}
