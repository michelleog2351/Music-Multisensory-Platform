/* 
 * Filename: LiveSpotifyDataService.cs
 * Description: Contains the definition of the LiveSpotifyDataService class.
 */

using MusicBased_IOT_Platform.Application.Services.Mock;
using MusicBased_IOT_Platform.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MusicBased_IOT_Platform.Application.Services.Live
{
    /// <summary>
    /// The <c>LiveSpotifyDataService</c> class implemented the methods defined by the
    /// ISpotifyDataService interface and retrieves live data from the the Spotify API.
    /// </summary>  
    public class LiveSpotifyDataService : IMockSpotifyDataService
    {
        // Fields
        // HttpClient used to make live calls to the spotify API
        private readonly HttpClient _httpClient;

        // Constructors
        /// <summary>
        /// LiveSpotifyDataService
        /// </summary>
        /// <param name="authorisationUrl"></param>
        /// <param name="url"></param>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        public LiveSpotifyDataService(string authorisationUrl, string url, string clientID, string clientSecret)
        {
            AccessToken = new AccessToken();
            AuthorisationUrl = authorisationUrl;
            BaseURL = url;
            ClientID = clientID;
            ClientSecret = clientSecret;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseURL)
            };
            InitialiseDataClient();
        }

        // Properties
        /// <summary>
        /// Access token used to access the spotify web api
        /// </summary>
        public AccessToken AccessToken { get; set; }

        /// <summary>
        /// AuthorisationUrl used to access the spotify web api
        /// </summary>
        public string AuthorisationUrl { get; set; }

        /// <summary>
        /// BaseURL used to access the spotify web api
        /// </summary>
        public string BaseURL { get; set; }

        /// <summary>
        /// ClientID used to access the spotify web api
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// ClientSecret used to access the spotify web api
        /// </summary>
        public string ClientSecret { get; set; }

        // Methods

        /// <summary>
        /// The <c>AuthoriseClient</c> method implemented the Spotify client credentials flow as
        /// documented here:
        /// https://developer.spotify.com/documentation/web-api/tutorials/client-credentials-flow
        /// </summary>
        /// <returns>A <c>bool</c> Authorisation has been granted.</returns>
        public bool AuthoriseClient()
        {
            HttpRequestMessage request = new(HttpMethod.Post, AuthorisationUrl + "?grant_type=client_credentials");

            // The authorisation string consisting of the ClientID and ClientSecret has to be
            // converted into a Base64 string for the Spotify authorisation request. 
            string auth_string = ClientID + ":" + ClientSecret;
            byte[] auth_bytes = Encoding.UTF8.GetBytes(auth_string);
            string auth_base64 = Convert.ToBase64String(auth_bytes);

            // Add the header information
            request.Headers.Add("Authorization", "Basic " + auth_base64);
            var content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            request.Content = content;
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a success code
                // https://developer.spotify.com/documentation/web-api/concepts/api-calls
                AccessToken = new AccessToken
                {
                    Token = "unable to acquire token"
                };
                return false;
            }

            string responseBody = response.Content.ReadAsStringAsync().Result;

            // Deserialise the JSON response into an AccessToken object
            // For details on deserialising JSON see:
            // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/deserialization

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            AccessToken = JsonSerializer.Deserialize<AccessToken>(responseBody, options)!;
            return true;
        }


        /// <summary>
        /// The <c>GetNewAlbumReleases</c> gets a list of new albums from the spotify web API
        /// </summary>
        /// <param name="limit">The maximum number of items to return.
        /// Minimum: 1
        /// Maximum: 50
        /// </param>
        /// <param name="offset"/>
        /// <returns><c>GetNewAlbumReleases</c> A list of newly released albums</returns>
        public NewReleases GetNewAlbumReleases(int limit = 20, int offset = 0)
        {
            // Check to see if the current token is still valid, if not get a new one
            if (!IsTokenStillValid() && !AuthoriseClient())
                return new NewReleases();

            // We have a valid token crack on with the request
            if (limit <= 0 || limit > 50)
                limit = 20;
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/browse/new-releases?limit=" + limit.ToString());
            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new NewReleases();
            }

            string responseBody = response.Content.ReadAsStringAsync().Result;
            NewReleases newReleases = JsonSerializer.Deserialize<NewReleases>(responseBody)!;

            return newReleases;
        }

        /// <summary>
        /// The <c>InitialiseDataClient()</c> method 
        /// </summary>
        /// <returns></returns>
        public bool InitialiseDataClient()
        {
            return AuthoriseClient();
        }

        /// <summary>
        /// The <c>IsTokenStillValid</c> method checks to see if the current token is still valid.
        /// </summary>
        /// <returns><c>true</c> if the current access token is valid otherwise <c>false</c></returns>
        public bool IsTokenStillValid()
        {
            if (AccessToken == null)
                return false;
            if (AccessToken.DateTimeAcquired.AddSeconds(AccessToken.ExpiresIn) > DateTime.UtcNow)
                return true;
            return false;
        }

        /// <summary>
        /// The <c>TestDataConnection</c> method tests the connection to the Spotify web API by
        /// Authorising the client and getting a new access token.
        /// </summary>
        /// <returns><c>true</c> if we have a live connection to the Spotify API service
        /// otherwise false.</returns>
        public bool TestDataConnection()
        {
            // We could make a dummy call but it is probably better to just re-Authorise the
            // client an get a new access token.
            return AuthoriseClient();
        }

        /// <summary>
        /// The <c>Search</c> method gets Spotify catalog information about albums, artists,
        /// playlists, tracks, shows, episodes or audiobooks that match a keyword string.
        /// </summary>
        /// <param name="searchQuery"><c>string</c> The values to search for</param>
        /// <param name="searchItemTypes"><c>string</c> The type of items</param>
        /// <returns><c>SearchResults</c> object </returns>
        public SearchResults Search(string searchQuery, string searchItemTypes)
        {
            // Check to see if the current token is still valid, if not get a new one
            if (!IsTokenStillValid() && !AuthoriseClient())
                return new SearchResults();

            // We have a valid token crack on with the request
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/search?q={searchQuery}&type={searchItemTypes}&market=IE&limit=5&offset=0");

            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list

                return new SearchResults();
            }
            string responseBody = response.Content.ReadAsStringAsync().Result;
            SearchResults searchItems = JsonSerializer.Deserialize<SearchResults>(responseBody)!;
            return searchItems;

        }


        /// <summary>
        /// The <c>GetAlbum()</c> method  gets Spotify catalog information for a single album.
        /// </summary>
        /// <param name="id"><c>string</c> representing the IDS of the albums</param>
        /// <param name="market"><c>string</c> an optional ISO 3166-1 alpha-2 country code</param>
        /// <returns>A single album</returns>
        public Album GetAlbum(string id, string market = "IE")
        {
            // Check to see if the current token is stioll valid, if not get a neww one
            if (!IsTokenStillValid() && !AuthoriseClient())
                return new Album();


            // We have a valid token crack on with the request

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/albums/{id}");
            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new Album();
            }

            string responseBody = response.Content.ReadAsStringAsync().Result;
            Album album = JsonSerializer.Deserialize<Album>(responseBody)!;
            return album;
        }


        /// <summary>
        /// The <c>GetAlbums</c> method gets the details of one or more
        /// albums with the specified IDs and optional market.
        /// </summary>
        /// <param name="ids"><c>string</c> representing the IDS of the albums</param>
        /// <param name="market"><c>string</c> an optional ISO 3166-1 alpha-2 country code.</param>
        /// <returns><c>List Album</c> A list of albums</returns>
        public List<Album> GetAlbums(string ids, string market = "IE")
        {
            if (!IsTokenStillValid() && !AuthoriseClient())
                return new List<Album>();


            // We have a valid token crack on with the request

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/albums?ids={ids}");
            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new List<Album>();
            }

            string responseBody = response.Content.ReadAsStringAsync().Result;
            ListAlbums albums = JsonSerializer.Deserialize<ListAlbums>(responseBody)!;
            return albums.Albums!; // Albums may return null
        }

        /// <summary>
        /// The <c>GetAlbumTracks</c> method returns a list of track objects containing the details
        /// of the tracks on the album. 
        /// </summary>
        /// <param name="ids">a <c>string</c> representing the ID of the album</param>
        /// <param name="market"><c>string</c> an optional ISO 3166-1 alpha-2 country code.</param>
        /// <param name="limit"><c>int</c> An optional int specifying the number of items to return.</param>
        /// <returns> <c>List Track</c> A list of tracks containing track details on the album</returns>
        public List<Track> GetAlbumTracks(string ids, string market = "IE", int limit = 20)
        {
            if (!IsTokenStillValid() && !AuthoriseClient())
                return new List<Track>();

            // We have a valid token crack on with the request

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/albums/{ids}/tracks");
            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new List<Track>();
            }

            string responseBody = response.Content.ReadAsStringAsync().Result;
            AlbumTracks albumTracks = JsonSerializer.Deserialize<AlbumTracks>(responseBody)!;
            return albumTracks.Tracks!; // Tracks may return null
        }

        /// <summary>
        /// The <c>GetArtist</c> method gets Spotify catalog information for a
        /// single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="id" >the Spotify IDs for the artist.</param>
        /// <returns><c>Artist</c> An artist object</returns>
        public Artist GetArtist(string id)
        {
            if (!IsTokenStillValid() && !AuthoriseClient())
                return new Artist();


            // We have a valid token crack on with the request

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/artists/" + id);
            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new Artist();
            }

            string responseBody = response.Content.ReadAsStringAsync().Result;
            Artist artist = JsonSerializer.Deserialize<Artist>(responseBody)!;
            return artist!; // Artist may return null
        }

        /// <summary>
        /// Get Spotify catalog information for a list of artists identified
        /// by their unique Spotify IDs.
        /// </summary>
        /// <param name="ids"><c>string</c> a comma-separated list of the Spotify IDs for
        /// the artists.</param>
        /// /// <param name="market"><c>string</c> market availability</param>
        /// /// <param name="limit"><c>int</c> restrict number of items</param>
        /// <returns> <c>List Artist</c>A list of one or more <c>Artist</c> objects.</returns>
        public List<Artist> GetArtists(string ids, string market = "IE", int limit = 20)
        {
            // Check to see if the current token is still valid, if not get a new one
            if (!IsTokenStillValid() && !AuthoriseClient())
                return new List<Artist>();


            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/artists?ids=" + ids);
            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new List<Artist>();
            }

            string responseBody = response.Content.ReadAsStringAsync().Result;
            ArtistsList artists = JsonSerializer.Deserialize<ArtistsList>(responseBody)!;
            return artists.Artists!;

        }

        /// <summary>
        /// <c>GetArtistsAlbums</c> Get Spotify catalog information about an artist's albums.
        /// </summary>
        /// <param name="id">A string containing the artist id</param>
        /// <param name="market">A string containing the market code</param>
        /// <param name="limit">An integer containg the number of record that will be returned</param>
        /// <returns><c>ArtistAlbums</c>A list of one or more <c>Artist</c> albums.</returns>
        public ArtistAlbums GetArtistsAlbums(string id, string market = "IE", int limit = 20)
        {
            // Check to see if the current token is still valid, if not get a new one
            if (!IsTokenStillValid() && !AuthoriseClient())
                return new ArtistAlbums();

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/artists/{id}/albums?market={market}&limit={limit}");
            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new ArtistAlbums();
            }
            string responseBody = response.Content.ReadAsStringAsync().Result;
            ArtistAlbums artistsAlbums = JsonSerializer.Deserialize<ArtistAlbums>(responseBody)!;
            return artistsAlbums;

        }

        /// <summary>
        /// The <c>GetArtistsTopTracks</c> gets Spotify catalog information about an artist's top tracks by country.
        /// </summary>
        /// <param name="id"> a string spotify ID for the artist.</param>
        /// <param name="market">an optional ISO 3166-1 alpha-2 country code.</param>
        /// <returns><c>ArtistTopTracks</c>a list of (10) top tracks for the artist.</returns>
        public ArtistTopTracks GetArtistsTopTracks(string id, string market = "IE")
        {
            // Check to see if the current token is still valid, if not get a new one
            if (!IsTokenStillValid())
                return new ArtistTopTracks();

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/artists/{id}/top-tracks?market={market}");
            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new ArtistTopTracks();
            }
            string responseBody = response.Content.ReadAsStringAsync().Result;
            ArtistTopTracks artistsTopTracks = JsonSerializer.Deserialize<ArtistTopTracks>(responseBody)!;
            return artistsTopTracks;

        }
        /// <summary>
        /// The <c>GetRelatedArtists</c> method gets Spotify catalog information about artists similar to a given artist.
        /// Similarity is based on analysis of the Spotify community's listening history.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Artist> GetRelatedArtists(string id) => throw new NotImplementedException();

        /// <summary>
        /// The <c>GetTrack</c> method returns a track from the spotify Web API
        /// </summary>
        /// <param name="id"><c>string</c> id of track </param>
        /// <param name="market"><c>string</c> market availability</param>
        /// <returns>A list of tracks</returns>
        public Track GetTrack(string id, string market = "IE")
        {

            // Check to see if the current token is still valid, if not get a new one
            if (!IsTokenStillValid() && !AuthoriseClient())
                return new Track();
            // We have a valid token crack on with the request

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/tracks/{id}");

            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new Track();
            }
            string responseBody = response.Content.ReadAsStringAsync().Result;
            Track track = JsonSerializer.Deserialize<Track>(responseBody)!;
            return track;

        }

        /// <summary>
        /// The <c>GetTracks()</c> method gets a list of tracks from the spotify Web API
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="market"></param>
        /// <returns><c>List Track</c>A list of tracks</returns>
        public List<Track> GetTracks(string ids, string market = "IE")
        {

            // Check to see if the current token is still valid, if not get a new one
            if (!IsTokenStillValid() && !AuthoriseClient())
                return new List<Track>();
            // We have a valid token crack on with the request

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/tracks?ids={ids}");
            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new List<Track>();
            }
            string responseBody = response.Content.ReadAsStringAsync().Result;
            Tracks tracks = JsonSerializer.Deserialize<Tracks>(responseBody)!;
            return tracks.TrackList!;

        }

        /// <summary>
        /// The <c>GetRecommendations</c> method is generated based on the
        /// available information for a given seed entity and matched against
        /// artists and tracks. If there is sufficient information about the
        /// provided seeds, a list of tracks will be returned together with pool
        /// size detail
        /// </summary>
        /// <param name="seedArtists"></param>
        /// <param name="seedGenres"></param>
        /// <param name="seedTracks"></param>
        /// <param name="limit"></param>
        /// <param name="market"></param>
        /// <returns> <c>Recommendations</c> object with tracks based on users mood</returns>
        public Recommendations GetRecommendations(string seedArtists, string seedGenres, string seedTracks, int limit = 10, string market = "IE")
        {
            // Check to see if the current token is still valid, if not get a new one
            if (!IsTokenStillValid() && !AuthoriseClient())
                return new Recommendations();
            // We have a valid token crack on with the request

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/recommendations"
                + "?seed_artists="
                + seedArtists
                + "&seed_genres=" //extract genre from whatever item it is?
                + seedGenres
                + "&seed_tracks="
                + seedTracks
                + "&limit="
                + limit
                + "&market="
                + market);

            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new Recommendations();
            }
            string responseBody = response.Content.ReadAsStringAsync().Result;
            Recommendations rec = JsonSerializer.Deserialize<Recommendations>(responseBody)!;
            return rec;
        }

        /// <summary>
        /// <c>GetRecommendedTracks</c> method gets recommended tracks based on user input
        /// </summary>
        /// <param name="id"><c>string</c>value for id</param>
        /// <returns> <c>Recommendations</c> object with recommended tracks based on users mood</returns>
        public Recommendations GetRecommendedTracks(string id)
        {
            // Check to see if the current token is stioll valid, if not get a neww one
            if (!IsTokenStillValid())
                return new Recommendations();

            // We have a valid token crack on with the request

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/recommendations?seed_tracks=" + id);
            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new Recommendations();
            }

            string responseBody = response.Content.ReadAsStringAsync().Result;
            Recommendations recTracks = JsonSerializer.Deserialize<Recommendations>(responseBody)!;
            return recTracks;
        }

        /// <summary>
        /// <c>GetRecommendedArtists</c> method gets recommended artists based on user input
        /// </summary>
        /// <param name="id"><c>string</c>value for id</param>
        /// <returns> <c>ArtistsList</c> object with tracks based on users mood</returns>
        public ArtistsList GetRecommendedArtists(string id)
        {
            // Check to see if the current token is still valid, if not get a new one
            if (!IsTokenStillValid())
                return new ArtistsList();
            // We have a valid token crack on with the request

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/artists/" + id + "/related-artists");
            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new ArtistsList();
            }

            string responseBody = response.Content.ReadAsStringAsync().Result;
            ArtistsList artists = JsonSerializer.Deserialize<ArtistsList>(responseBody)!;
            return artists; // TrackList may return null
        }

        /// <summary>
        /// <c>GetSeedGenres</c> returns a list of grnres
        /// </summary>
        /// <returns><c>List string</c></returns>
        public List<string> GetSeedGenres()
        {
            // Check to see if the current token is still valid, if not get a new one
            if (!IsTokenStillValid() && !AuthoriseClient())
                return new List<string>();
            // We have a valid token crack on with the request

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/recommendations/available-genre-seeds");
            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new List<string>();
            }

            string responseBody = response.Content.ReadAsStringAsync().Result;
            List<string> genres = JsonSerializer.Deserialize<Genre>(responseBody)!.Genres!;
            return genres; // TrackList may return null
        }


        /// <summary>
        /// The <c>GetMoodRecommendations</c> method is used to get return
        /// recommendations based on what the users current mood
        /// </summary>
        /// <param name="limit"><c>int</c> limit to the number of records returned</param>
        /// <param name="max_danceability"><c>double</c>value for danceability</param>
        /// <param name="max_energy"><c>double</c>value for energy</param>
        /// <param name="max_valence"><c>double</c>value for valence</param>
        /// <param name="max_liveness"><c>double</c>value for liveness</param>
        /// <returns> <c>Reccomendations</c> object with artists based on users mood</returns>
        public Recommendations GetMoodRecommendations(int limit, double max_danceability, double max_energy, double max_valence, double max_liveness)
        {

            // Check to see if the current token is still valid, if not get a new one
            if (!IsTokenStillValid())
                return new Recommendations();

            List<string> genres = GetSeedGenres();
            StringBuilder stringBuilder = new();
            int i = 0;
            foreach (string genre in genres)
            {
                i++;
                if (i == 5)
                    break;
                stringBuilder.Append($"{genre},");

            }

            // We have a valid token crack on with the request

            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.spotify.com/v1/recommendations?limit={limit}&seed_genres={stringBuilder.ToString()}&max_danceability={max_danceability}&max_energy={max_energy}&max_valence={max_valence}&max_liveness={max_liveness}");
            request.Headers.Add("Authorization", "Bearer " + AccessToken.Token);
            Task<HttpResponseMessage> task = _httpClient.SendAsync(request);
            HttpResponseMessage response = task.Result;
            try
            {
                // throw an exception if we didn't get a valid response
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return new Recommendations();
            }

            string responseBody = response.Content.ReadAsStringAsync().Result;
            Recommendations moods = JsonSerializer.Deserialize<Recommendations>(responseBody)!;
            return moods; // TrackList may return null

        }


    }

}
