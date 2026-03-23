/* 
 * Filename: LiveSpotifyDataService.cs
 * Description: Contains the definition of the LiveSpotifyDataService class.
 */

using Microsoft.Extensions.Options;
using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Application.Interfaces.Spotify;
using MusicBased_IOT_Platform.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MusicBased_IOT_Platform.Application.Services.Spotify.Live
{
    /// <summary>
    /// The <c>LiveSpotifyDataService</c> class implemented the methods defined by the
    /// ISpotifyDataService interface and retrieves live data from the the Spotify API.
    /// </summary>  
    public class LiveSpotifyDataService : ISpotifyDataService
    {
        // Fields
        // HttpClient used to make live calls to the spotify API
        private readonly HttpClient _httpClient;
        private readonly SpotifySettings _settings;
        private readonly IUserRepository _userRepo;
        private readonly IUserContext _userContext;

        /// <summary>
        /// The JsonSerializerOptions object is used to specify options for the JSON serializer
        /// PropertyNameCaseInsensitive option to true allowing for the deserialisation of JSON responses
        /// without being case-sensitive to the property names
        /// </summary>
        private static readonly JsonSerializerOptions _jsonOptions =
            new()
            {
                PropertyNameCaseInsensitive = true
            };


        /// <summary>
        /// The constructor for the <c>LiveSpotifyDataService</c> class takes an HttpClient and AppSettings as parameters and initialises the class fields and properties.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="settings"></param>
        public LiveSpotifyDataService(
            HttpClient httpClient,
            IOptions<SpotifySettings> settings, IUserRepository userRepo, IUserContext userContext)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _userRepo = userRepo;
            _userContext = userContext;

            AccessToken = new AccessToken();

            AuthorisationUrl = _settings.AuthorisationUrl;
            BaseURL = _settings.BaseURL;
            ClientID = _settings.ClientID ?? string.Empty;
            ClientSecret = _settings.ClientSecret;

            _httpClient.BaseAddress = new Uri(_settings.BaseURL);
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
        public string GetSpotifyLoginUrl()
        {
            return "https://accounts.spotify.com/authorize" +
                   "?client_id=" + ClientID +
                   "&response_type=code" +
                   "&redirect_uri=https://localhost:7039/signin-spotify" +
                   "&scope=user-read-playback-state user-modify-playback-state streaming";
        }

        /// <summary>
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task ExchangeCodeAsync(string code)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"https://accounts.spotify.com/api/token");

            var authString = $"{ClientID}:{ClientSecret}";
            var authBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(authString));

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authBase64);

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", "https://localhost:7039/signin-spotify" }
            });

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<AccessToken>(json, _jsonOptions)!;

            AccessToken = token;

            var user = await _userContext.GetCurrentUserAsync();

            if (user != null)
            {
                user.SpotifyAccessToken = token.Token;
                user.SpotifyRefreshToken = token.RefreshToken;
                user.SpotifyTokenExpiry =
                    DateTime.UtcNow.AddSeconds(token.ExpiresIn);

                await _userRepo.UpdateAsync(user);
            }
        }

        private async Task<T> GetAsync<T>(string endpoint)
        {
            if (string.IsNullOrEmpty(AccessToken.Token))
            {
                await LoadTokenFromDatabaseAsync();
            }

            var hasToken = await HasValidTokenAsync();

            if (!hasToken || string.IsNullOrEmpty(AccessToken.Token))
            {
                throw new InvalidOperationException("No valid Spotify access token available.");
            }

            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    AccessToken.Token);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(json, _jsonOptions)!;
        }

        private async Task LoadTokenFromDatabaseAsync()
        {
            var user = await _userContext.GetCurrentUserAsync();

            if (user == null || string.IsNullOrEmpty(user.SpotifyAccessToken))
                return;

            AccessToken = new AccessToken
            {
                Token = user.SpotifyAccessToken,
                RefreshToken = user.SpotifyRefreshToken ?? "",
                ExpiresIn = (int)((user.SpotifyTokenExpiry ?? DateTime.UtcNow) - DateTime.UtcNow).TotalSeconds,
                DateTimeAcquired = DateTime.UtcNow
            };
        }

        /// <summary>
        /// The HasValidTokenAsync method checks if the current access token is valid by verifying that it exists and has not expired. 
        /// If the token has expired, it attempts to refresh the access token using the refresh token. 
        /// The method returns true if a valid access token is available, and false otherwise.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> HasValidTokenAsync()
        {
            if (AccessToken == null || string.IsNullOrEmpty(AccessToken.Token))
                return false;

            var expiryTime =
                AccessToken.DateTimeAcquired.AddSeconds(AccessToken.ExpiresIn);

            if (DateTime.UtcNow >= expiryTime)
            {
                await RefreshAccessTokenAsync();
            }

            return true;
        }

        /// <summary>
        /// The RefreshAccessTokenAsync method is responsible for refreshing the access token when it has expired. 
        /// It sends a POST request to the Spotify Web API with the refresh token to obtain a new access token, 
        /// and updates the AccessToken property with the new token information.
        /// </summary>
        /// <returns></returns>
        private async Task RefreshAccessTokenAsync()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"https://accounts.spotify.com/api/token");

            var authString = $"{ClientID}:{ClientSecret}";
            var authBytes = Encoding.UTF8.GetBytes(authString);
            var authBase64 = Convert.ToBase64String(authBytes);

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authBase64);

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", AccessToken.RefreshToken }
            });

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            AccessToken =
                JsonSerializer.Deserialize<AccessToken>(json, _jsonOptions)!;

            AccessToken.DateTimeAcquired = DateTime.UtcNow;

            var user = await _userContext.GetCurrentUserAsync();

            if (user != null)
            {
                user.FitbitAccessToken = AccessToken.Token;
                user.FitbitRefreshToken = AccessToken.RefreshToken;
                user.FitbitTokenExpiry =
                    DateTime.UtcNow.AddSeconds(AccessToken.ExpiresIn);

                await _userRepo.UpdateAsync(user);
            }
        }

        /// <summary>
        /// The <c>AuthoriseClientAsync</c> method authorises the client to access the spotify web API and gets an access token
        /// </summary>
        /// <returns>A <c>bool</c> Authorisation has been granted.</returns>
        public async Task<bool> AuthoriseClientAsync()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                AuthorisationUrl);

            // The authorisation string consisting of the ClientID and ClientSecret has to be
            // converted into a Base64 string for the Spotify authorisation request. 
            string auth_string = $"{ClientID}:{ClientSecret}";
            byte[] auth_bytes = Encoding.UTF8.GetBytes(auth_string);
            string auth_base64 = Convert.ToBase64String(auth_bytes);

            // Add the header information
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", auth_base64);

            //request.Content = new StringContent(
            //    "grant_type=client_credentials",
            //    Encoding.UTF8,
            //    "application/x-www-form-urlencoded");
            request.Content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" }
                });

            // Make the request and get the response, throw an exception if we don't get a valid response
            HttpResponseMessage response;
            try
            {
                // Throw an exception if valid response isn't received
                response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // Didn't get a success code

                Console.WriteLine("Spotify token request failed");

                AccessToken = new AccessToken
                {
                    Token = "unable to acquire token"
                };
                return false;
            }

            // Read the response body as a string
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{responseBody}");

            // Deserialise the JSON response into an AccessToken object
            // For details on deserialising JSON see:
            // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/deserialization

            AccessToken =
                JsonSerializer.Deserialize<AccessToken>(responseBody, _jsonOptions)!;
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
        //public async Task<NewReleases> GetNewAlbumReleases(int limit = 20, int offset = 0)
        //{
        //    // Check to see if the current token is still valid, if not get a new one
        //    if (!IsTokenStillValid())
        //    {
        //        bool authorised = await AuthoriseClientAsync();
        //        if (!authorised)
        //            return new NewReleases();
        //    }

        //    // We have a valid token crack on with the request
        //    if (limit <= 0 || limit > 50)
        //        limit = 20;

        //    var request = new HttpRequestMessage(
        //        HttpMethod.Get,
        //        $"https://api.spotify.com/v1/browse/new-releases?limit={limit}");

        //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken.Token);

        //    HttpResponseMessage response;

        //    try
        //    {
        //        // throw an exception if we didn't get a valid response
        //        response = await _httpClient.SendAsync(request);
        //        response.EnsureSuccessStatusCode();
        //    }
        //    catch (HttpRequestException)
        //    {
        //        // Didn't get a valid response, return an empty list
        //        return new NewReleases();
        //    }

        //    string responseBody = await response.Content.ReadAsStringAsync();

        //    return JsonSerializer.Deserialize<NewReleases>(responseBody)!;
        //}

        public async Task<NewReleases> GetNewAlbumReleases(int limit = 20, int offset = 0)
        {
            if (limit <= 0 || limit > 50)
                limit = 20;

            try
            {
                return await GetAsync<NewReleases>(
                    $"https://api.spotify.com/v1/browse/new-releases?limit={limit}");
            }
            catch
            {
                // Didn't get a valid response, return an empty list
                return new NewReleases();
            }
        }

        /// <summary>
        /// The <c>InitialiseDataClient()</c> method 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InitialiseDataClient()
        {
            return await AuthoriseClientAsync();
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
        public async Task<bool> TestDataConnection()
        {
            // re-authorise the client and get a new access token.
            return await AuthoriseClientAsync();
        }

        /// <summary>
        /// The <c>Search</c> method gets Spotify catalog information about albums, artists,
        /// playlists, tracks, shows, episodes or audiobooks that match a keyword string.
        /// </summary>
        /// <param name="searchQuery"><c>string</c> The values to search for</param>
        /// <param name="searchItemTypes"><c>string</c> The type of items</param>
        /// <returns><c>SearchResults</c> object </returns>
        public async Task<SearchResults> Search(string searchQuery, string searchItemTypes)
        {
            try
            {
                return await GetAsync<SearchResults>($"https://api.spotify.com/v1/search?q={Uri.EscapeDataString(searchQuery)}&type={searchItemTypes}&market=IE&limit=5&offset=0");
            }
            catch
            {
                return new SearchResults();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackUri"></param>
        /// <returns></returns>
        public async Task PlayTrack(string trackUri)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Put,
                $"https://api.spotify.com/v1/me/player/play");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", AccessToken.Token);

            request.Content = new StringContent(
                JsonSerializer.Serialize(new
                {
                    uris = new[] { trackUri }
                }),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// The <c>GetAlbum()</c> method  gets Spotify catalog information for a single album.
        /// </summary>
        /// <param name="id"><c>string</c> representing the IDS of the albums</param>
        /// <param name="market"><c>string</c> an optional ISO 3166-1 alpha-2 country code</param>
        /// <returns>A single album</returns>
        public async Task<Album> GetAlbum(string id, string market = "IE")
        {
            try
            {

                return await GetAsync<Album>(
                    $"https://api.spotify.com/v1/albums/{id}");
            }
            catch
            {
                return new Album();
            }
        }

        /// <summary>
        /// The <c>GetAlbums</c> method gets the details of one or more
        /// albums with the specified IDs and optional market.
        /// </summary>
        /// <param name="ids"><c>string</c> representing the IDS of the albums</param>
        /// <param name="market"><c>string</c> an optional ISO 3166-1 alpha-2 country code.</param>
        /// <returns><c>List Album</c> A list of albums</returns>
        public async Task<List<Album>> GetAlbums(string ids, string market = "IE")
        {
            try
            {
                return await GetAsync<List<Album>>(
                    $"https://api.spotify.com/v1/albums?ids={ids}");
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return [];
            }
        }

        /// <summary>
        /// The <c>GetAlbumTracks</c> method returns a list of track objects containing the details
        /// of the tracks on the album. 
        /// </summary>
        /// <param name="ids">a <c>string</c> representing the ID of the album</param>
        /// <param name="market"><c>string</c> an optional ISO 3166-1 alpha-2 country code.</param>
        /// <param name="limit"><c>int</c> An optional int specifying the number of items to return.</param>
        /// <returns> <c>List Track</c> A list of tracks containing track details on the album</returns>
        public async Task<List<Track>> GetAlbumTracks(
            string ids,
            string market = "IE",
            int limit = 20)
        {
            if (limit <= 0 || limit > 50)
                limit = 20;


            try
            {
                return await GetAsync<List<Track>>(
                $"https://api.spotify.com/v1/albums/{ids}/tracks");

            }
            catch
            {
                // Return an empty list
                return [];
            }
        }

        /// <summary>
        /// The <c>GetArtist</c> method gets Spotify catalog information for a
        /// single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="id" >the Spotify IDs for the artist.</param>
        /// <returns><c>Artist</c> An artist object</returns>
        public async Task<Artist> GetArtist(string id)
        {
            try
            {
                return await GetAsync<Artist>(
                $"https://api.spotify.com/v1/artists/{id}");
            }
            catch
            {
                // Valid response isn't received, return an empty list
                return new Artist();
            }
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
        public async Task<List<Artist>> GetArtists(
            string ids,
            string market = "IE",
            int limit = 20)
        {

            try
            {
                return await GetAsync<List<Artist>>(
                $"https://api.spotify.com/v1/artists?ids={ids}");

            }
            catch
            {
                // Valid response, return an empty list
                return [];
            }
        }

        /// <summary>
        /// <c>GetArtistsAlbums</c> Get Spotify catalog information about an artist's albums.
        /// </summary>
        /// <param name="id">A string containing the artist id</param>
        /// <param name="market">A string containing the market code</param>
        /// <param name="limit">An integer containg the number of record that will be returned</param>
        /// <returns><c>ArtistAlbums</c>A list of one or more <c>Artist</c> albums.</returns>
        public async Task<ArtistAlbums> GetArtistsAlbums(string id, string market = "IE", int limit = 20)
        {

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.spotify.com/v1/artists/{id}/albums?market={market}&limit={limit}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken.Token);

            HttpResponseMessage response;

            try
            {
                // throw an exception if valid response isn't received
                response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // Valid response, return an empty list
                return new ArtistAlbums();
            }
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ArtistAlbums>(responseBody)!;
        }

        /// <summary>
        /// The <c>GetArtistsTopTracks</c> gets Spotify catalog information about an artist's top tracks by country.
        /// </summary>
        /// <param name="id"> a string spotify ID for the artist.</param>
        /// <param name="market">an optional ISO 3166-1 alpha-2 country code.</param>
        /// <returns><c>ArtistTopTracks</c>a list of (10) top tracks for the artist.</returns>
        public async Task<ArtistTopTracks> GetArtistsTopTracks(
            string id,
            string market = "IE")
        {

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.spotify.com/v1/artists/{id}/top-tracks?market={market}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken.Token);

            HttpResponseMessage response;

            try
            {
                // throw an exception if valid response isn't received
                response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // Didn't get a valid response, return an empty list
                return new ArtistTopTracks();
            }
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ArtistTopTracks>(responseBody)!;
        }

        /// <summary>
        /// The <c>GetRelatedArtists</c> method gets Spotify catalog information about artists similar to a given artist.
        /// Similarity is based on analysis of the Spotify community's listening history.
        /// </summary>
        /// <param name="id"></param>
        /// <return><c>List Artist</c>A list of artists similar to the given artist.</returns>
        public async Task<List<Artist>> GetRelatedArtists(string id)
        {

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.spotify.com/v1/artists/{id}/related-artists");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken.Token);

            HttpResponseMessage response;

            try
            {
                // throw an exception if valid response isn't received
                response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // Didn't get a valid response, return an empty list
                return [];
            }

            string responseBody = await response.Content.ReadAsStringAsync();
            ArtistsList artists = JsonSerializer.Deserialize<ArtistsList>(responseBody)!;
            return artists.Artists!; // Artists may return null 
        }

        /// <summary>
        /// The <c>GetTrack</c> method returns a track from the spotify Web API
        /// </summary>
        /// <param name="id"><c>string</c> id of track </param>
        /// <param name="market"><c>string</c> market availability</param>
        /// <returns>A list of tracks</returns>
        public async Task<Track> GetTrack(string id,
            string market = "IE")
        {

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.spotify.com/v1/tracks/{id}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken.Token);

            HttpResponseMessage response;

            try
            {
                // throw an exception if valid response isn't received
                response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // Didn't get a valid response, return an empty list
                return new Track();
            }
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Track>(responseBody)!;
        }

        /// <summary>
        /// The <c>GetTracks()</c> method gets a list of tracks from the spotify Web API
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="market"></param>
        /// <returns><c>List Track</c>A list of tracks</returns>
        public async Task<List<Track>> GetTracks(
            string ids,
            string market = "IE")
        {

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.spotify.com/v1/tracks?ids={ids}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken.Token);

            HttpResponseMessage response;
            try
            {
                // throw an exception if valid response isn't received
                response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // We didn't get a valid response, return an empty list
                return [];
            }
            string responseBody = await response.Content.ReadAsStringAsync();

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
        public async Task<Recommendations> GetRecommendations(
            string seedArtists,
            string seedGenres,
            string seedTracks,
            int limit = 10,
            string market = "IE")
        {

            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.spotify.com/v1/recommendations?seed_artists={seedArtists}&seed_genres={seedGenres}&seed_tracks={seedTracks}&limit={limit}&market={market}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken.Token);

            HttpResponseMessage response;

            try
            {
                // throw an exception if valid response isn't received
                response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // Didn't get a valid response, return an empty list
                return new Recommendations();
            }
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Recommendations>(responseBody)!;
        }

        /// <summary>
        /// <c>GetRecommendedTracks</c> method gets recommended tracks based on user input
        /// </summary>
        /// <param name="id"><c>string</c>value for id</param>
        /// <returns> <c>Recommendations</c> object with recommended tracks based on users mood</returns>
        public async Task<Recommendations> GetRecommendedTracks(string id)
        {

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.spotify.com/v1/recommendations?seed_tracks={id}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken.Token);

            HttpResponseMessage response;
            try
            {
                // throw an exception if valid response isn't received
                response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // Didn't get a valid response, return an empty list
                return new Recommendations();
            }

            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Recommendations>(responseBody)!;
        }

        /// <summary>
        /// <c>GetSeedGenres</c> returns a list of available genres seed values for recommendations
        /// </summary>
        /// <returns><c>List string</c> A list of available genres seed values for recommendations</returns>
        public async Task<List<string>> GetSeedGenres()
        {

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.spotify.com/v1/recommendations/available-genre-seeds");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken.Token);

            HttpResponseMessage response;
            try
            {
                // throw an exception if valid response isn't received
                response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                // Didn't get a valid response, return an empty list
                return [];
            }

            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Genre>(responseBody)!.Genres!;
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
        public async Task<Recommendations> GetMoodRecommendations(
            int limit,
            double max_danceability,
            double max_energy,
            double max_valence,
            double max_liveness)
        {

            //var genres = await GetSeedGenres();
            //var seedGenres = string.Join(",", genres.Take(5));
            var seedGenres = "pop,rock,hip-hop,edm,chill,folk-hop,lofi";

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                 $"https://api.spotify.com/v1/recommendations?" +
                 $"limit={limit}&seed_genres={seedGenres}" +
                 $"&max_danceability={max_danceability}" +
                 $"&max_energy={max_energy}" +
                 $"&max_valence={max_valence}" +
                 $"&max_liveness={max_liveness}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken.Token);

            //HttpResponseMessage response;
            try
            {
                // throw an exception if valid response isn't received
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<Recommendations>(responseBody)!;
            }
            catch (HttpRequestException)
            {
                return new Recommendations();
            }

            //string responseBody = await response.Content.ReadAsStringAsync();
            //Recommendations moods = JsonSerializer.Deserialize<Recommendations>(responseBody)!;
            //return moods; // TrackList may return null
        }
    }

}
