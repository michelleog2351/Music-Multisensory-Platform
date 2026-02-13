using MusicBased_IOT_Platform.Application.Interfaces.Spotify;
using MusicBased_IOT_Platform.Models;
using System.Text.Json;

namespace MusicBased_IOT_Platform.Application.Services.Spotify.Mock
{
    public class MockSpotifyDataService : ISpotifyDataService
    {
        private readonly string _jsonFilePath;

        /// <summary>
        /// The constructor of the MockSpotifyDataService class initialises the _jsonFilePath field to the path where the JSON files containing the mock data are located.
        /// </summary>
        public MockSpotifyDataService()
        {
            _jsonFilePath = Path.Combine(
                AppContext.BaseDirectory,
                "Application",
                "Services",
                "Mock",
                "TestData"
                );
        }

        /// <summary>
        /// The GetTrack method is used to retrieve information about a track based on its ID. It loads the track data from a JSON file and returns a Track object containing the information about the track.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="market"></param>
        /// <returns></returns>
        public Track GetTrack(string id, string market = "IE")
        {
            var filePath = Path.Combine(_jsonFilePath, "track.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Track>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
        }

        /// <summary>
        /// The GetTracks method is used to retrieve information about multiple tracks based on their IDs. 
        /// It loads the track data from a JSON file and returns a list of Track objects containing the information about the tracks.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="market"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public List<Track> GetTracks(string ids, string market = "IE")
        {
            var filePath = Path.Combine(_jsonFilePath, "tracks.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Track>>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
        }

        /// <summary>
        /// This method is used to get an album by its ID. It loads the album data from a JSON file and returns an Album object. 
        /// The market parameter is used to specify the market for which the album data should be retrieved. 
        /// In this mock implementation, the market parameter is not used and the same album data is returned regardless of the market specified.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="market"></param>
        /// <returns></returns>
        public Album GetAlbum(string id, string market = "IE")
        {
            // Load from JSON file
            var filePath = Path.Combine(_jsonFilePath, "album.json");

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Album>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
        }

        /// <summary>
        /// This method is used to get multiple albums by their IDs. 
        /// It loads the album data from a JSON file and returns a list of Album objects.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="market"></param>
        /// <returns></returns>
        public List<Album> GetAlbums(string ids, string market = "IE")
        {
            // Load from JSON file
            var filePath = Path.Combine(_jsonFilePath, "albums.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Album>>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
        }

        /// <summary>
        /// This method is used to get the tracks on an album by the album's ID. It loads the track data from a JSON file and returns a list of Track objects.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="market"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Track> GetAlbumTracks(string ids, string market = "IE", int limit = 20)
        {
            // Load from JSON file
            var filePath = Path.Combine(_jsonFilePath, "album_tracks.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Track>>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
        }

        /// <summary>
        /// The GetArtist method is used to retrieve an artist's information based on their ID. 
        /// It loads the artist data from a JSON file and returns an Artist object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> An Artist object containing the artist's information.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        internal Artist GetArtist(string id)
        {
            // Load from JSON file
            var filePath = Path.Combine(_jsonFilePath, "artist.json");

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Artist>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
        }

        /// <summary>
        /// The GetArtists method is used to retrieve information about multiple artists based on their IDs.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="market"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public List<Artist> GetArtists(string ids, string market = "IE", int limit = 20)
        {
            // Load from JSON file
            var filePath = Path.Combine(_jsonFilePath, "artists.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Artist>>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
        }

        /// <summary>
        /// The GetArtistsAlbums method is used to retrieve the albums of an artist based on the artist's ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="market"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public ArtistAlbums GetArtistsAlbums(string id, string market = "IE", int limit = 20)
        {
            var filePath = Path.Combine(_jsonFilePath, "artist_albums.json");

            if (!File.Exists(filePath)) 
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ArtistAlbums>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
        }

        /// <summary>
        /// The GetArtistsTopTracks method is used to retrieve the top tracks of an artist based on the artist's ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="market"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public ArtistTopTracks GetArtistsTopTracks(string id, string market = "IE")
        {
            var filePath = Path.Combine(_jsonFilePath, "artist_top_tracks.json");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ArtistTopTracks>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
        }

        /// <summary>
        /// The GetNewAlbumReleases method is used to retrieve the new album releases. 
        /// It loads the new releases data from a JSON file and returns a NewReleases object containing the information about the new album releases.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public NewReleases GetNewAlbumReleases(int limit = 20, int offset = 0)
        {
            var filePath = Path.Combine(_jsonFilePath, "new_releases.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<NewReleases>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
        }

        public ArtistsList GetRecommendedArtists(string id)
        {
            throw new NotImplementedException();
        }

        public Recommendations GetRecommendedTracks(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetRelatedArtists method is used to retrieve a list of artists that are related to a given artist based on the artist's ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public List<Artist> GetRelatedArtists(string id)
        {
            var filePath = Path.Combine(_jsonFilePath, "related_artists.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Artist>>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
        }

        /// <summary>
        /// The GetSeedGenres method is used to retrieve a list of seed genres that can be used for generating recommendations.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public List<string> GetSeedGenres()
        {
            var filePath = Path.Combine(_jsonFilePath, "seed_genres.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<string>>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
        }

        static Recommendations GetMoodRecommendations(int limit, double max_danceability, double max_energy, double max_valence, double max_liveness)
        {
            throw new NotImplementedException();
        }


        static Recommendations GetRecommendations(string seedArtists, string seedGenres, string seedTracks, int limit, string market)
        {
            throw new NotImplementedException();
        }

        static SearchResults Search(string searchQuery, string searchItemTypes)
        {
            throw new NotImplementedException();
        }

        static bool TestDataConnection()
        {
            throw new NotImplementedException();
        }

        Artist ISpotifyDataService.GetArtist(string id)
        {
            return GetArtist(id);
        }

        Recommendations ISpotifyDataService.GetMoodRecommendations(int limit, double max_danceability, double max_energy, double max_valence, double max_liveness)
        {
            return GetMoodRecommendations(limit, max_danceability, max_energy, max_valence, max_liveness);
        }

        Recommendations ISpotifyDataService.GetRecommendations(string seedArtists, string seedGenres, string seedTracks, int limit, string market)
        {
            return GetRecommendations(seedArtists, seedGenres, seedTracks, limit, market);
        }

        SearchResults ISpotifyDataService.Search(string searchQuery, string searchItemTypes)
        {
            return Search(searchQuery, searchItemTypes);
        }

        bool ISpotifyDataService.TestDataConnection()
        {
            return TestDataConnection();
        }
    }
}
