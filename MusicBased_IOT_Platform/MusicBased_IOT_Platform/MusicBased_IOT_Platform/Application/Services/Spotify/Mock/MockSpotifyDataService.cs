/* 
 * Filename: MockSpotifyDataService.cs
 * Description: Contains the definition of the MockSpotifyDataService class.
 */


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
                "Spotify",
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
        public Task<Track> GetTrack(string id, string market = "IE")
        {
            var filePath = Path.Combine(_jsonFilePath, "track.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);

            var track = JsonSerializer.Deserialize<Track>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;

            return Task.FromResult(track);
        }

        /// <summary>
        /// The GetTracks method is used to retrieve information about multiple tracks based on their IDs. 
        /// It loads the track data from a JSON file and returns a list of Track objects containing the information about the tracks.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="market"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Task<List<Track>> GetTracks(string ids, string market = "IE")
        {
            var filePath = Path.Combine(_jsonFilePath, "tracks.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);

            var tracks = JsonSerializer.Deserialize<List<Track>>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;

            return Task.FromResult(tracks);
        }

        /// <summary>
        /// This method is used to get an album by its ID. It loads the album data from a JSON file and returns an Album object. 
        /// The market parameter is used to specify the market for which the album data should be retrieved. 
        /// In this mock implementation, the market parameter is not used and the same album data is returned regardless of the market specified.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="market"></param>
        /// <returns></returns>
        public Task<Album> GetAlbum(string id, string market = "IE")
        {
            // Load from JSON file
            var filePath = Path.Combine(_jsonFilePath, "album.json");

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);

            var album = JsonSerializer.Deserialize<Album>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
            return Task.FromResult(album);
        }

        /// <summary>
        /// This method is used to get multiple albums by their IDs. 
        /// It loads the album data from a JSON file and returns a list of Album objects.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="market"></param>
        /// <returns></returns>
        public Task<List<Album>> GetAlbums(string ids, string market = "IE")
        {
            // Load from JSON file
            var filePath = Path.Combine(_jsonFilePath, "albums.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            var albums = JsonSerializer.Deserialize<List<Album>>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
            return Task.FromResult(albums);
        }

        /// <summary>
        /// This method is used to get the tracks on an album by the album's ID. It loads the track data from a JSON file and returns a list of Track objects.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="market"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<List<Track>> GetAlbumTracks(string ids, string market = "IE", int limit = 20)
        {
            // Load from JSON file
            var filePath = Path.Combine(_jsonFilePath, "album_tracks.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            var tracks = JsonSerializer.Deserialize<List<Track>>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
            return Task.FromResult(tracks);
        }

        /// <summary>
        /// The GetArtist method is used to retrieve an artist's information based on their ID. 
        /// It loads the artist data from a JSON file and returns an Artist object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> An Artist object containing the artist's information.</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Task<Artist> GetArtist(string id)
        {
            // Load from JSON file
            var filePath = Path.Combine(_jsonFilePath, "artist.json");

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            var artist = JsonSerializer.Deserialize<Artist>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
            return Task.FromResult(artist);
        }

        /// <summary>
        /// The GetArtists method is used to retrieve information about multiple artists based on their IDs.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="market"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Task<List<Artist>> GetArtists(string ids, string market = "IE", int limit = 20)
        {
            // Load from JSON file
            var filePath = Path.Combine(_jsonFilePath, "artists.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            var artists = JsonSerializer.Deserialize<List<Artist>>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
            return Task.FromResult(artists);
        }

        /// <summary>
        /// The GetArtistsAlbums method is used to retrieve the albums of an artist based on the artist's ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="market"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Task<ArtistAlbums> GetArtistsAlbums(string id, string market = "IE", int limit = 20)
        {
            var filePath = Path.Combine(_jsonFilePath, "artist_albums.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            var artistAlbums = JsonSerializer.Deserialize<ArtistAlbums>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
            return Task.FromResult(artistAlbums);
        }

        /// <summary>
        /// The GetArtistsTopTracks method is used to retrieve the top tracks of an artist based on the artist's ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="market"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Task<ArtistTopTracks> GetArtistsTopTracks(string id, string market = "IE")
        {
            var filePath = Path.Combine(_jsonFilePath, "artist_top_tracks.json");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            var artistopTracks = JsonSerializer.Deserialize<ArtistTopTracks>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
            return Task.FromResult(artistopTracks);
        }

        /// <summary>
        /// The GetNewAlbumReleases method is used to retrieve the new album releases. 
        /// It loads the new releases data from a JSON file and returns a NewReleases object containing the information about the new album releases.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Task<NewReleases> GetNewAlbumReleases(int limit = 20, int offset = 0)
        {
            var filePath = Path.Combine(_jsonFilePath, "new_releases.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            var newReleases = JsonSerializer.Deserialize<NewReleases>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
            return Task.FromResult(newReleases);
        }

        public Task<Recommendations> GetRecommendedTracks(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetRelatedArtists method is used to retrieve a list of artists that are related to a given artist based on the artist's ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Task<List<Artist>> GetRelatedArtists(string id)
        {
            var filePath = Path.Combine(_jsonFilePath, "related_artists.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            var relatedArtists = JsonSerializer.Deserialize<List<Artist>>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
            return Task.FromResult(relatedArtists);
        }

        /// <summary>
        /// The GetSeedGenres method is used to retrieve a list of seed genres that can be used for generating recommendations.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public Task<List<string>> GetSeedGenres()
        {
            var filePath = Path.Combine(_jsonFilePath, "seed_genres.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            var seed_genres = JsonSerializer.Deserialize<List<string>>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
            return Task.FromResult(seed_genres);
        }

        public Task<Recommendations> GetMoodRecommendations(int limit, double max_danceability, double max_energy, double max_valence, double max_liveness)
        {
            throw new NotImplementedException();
        }


        public Task<Recommendations> GetRecommendations(string seedArtists, string seedGenres, string seedTracks, int limit, string market)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The SearchResults 
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="searchItemTypes"></param>
        /// <returns></returns>
        public async Task<SearchResults> Search(string searchQuery, string searchItemTypes)
        {
            var json = await File.ReadAllTextAsync("search_all_results.json");

            var results = JsonSerializer.Deserialize<SearchResults>
                (
                   json,
                   new JsonSerializerOptions
                   {
                       PropertyNameCaseInsensitive = true
                   })!;

            if (results.Tracks?.Items != null)
            {
                results.Tracks.Items = results.Tracks.Items
                    .Where(t => t.Name!.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList()!;
            }

            return results;
        }

        /// <summary>
        /// The TestDataConnection 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> TestDataConnection()
        {
            throw new NotImplementedException();
        }

        public Task PlayTrack(string trackUri)
        {
            throw new NotImplementedException();
        }
    }
}
