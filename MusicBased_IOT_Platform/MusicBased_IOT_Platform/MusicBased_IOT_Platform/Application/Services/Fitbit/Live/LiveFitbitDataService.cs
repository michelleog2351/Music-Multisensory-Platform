using Microsoft.AspNetCore.Connections.Features;
using Microsoft.Extensions.Options;
using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Application.Interfaces.Fitbit;
using MusicBased_IOT_Platform.Models;
using System.Text;
using System.Text.Json;

namespace MusicBased_IOT_Platform.Application.Services.Fitbit.Live
{
    public class LiveFitbitDataService : IFitbitDataService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;

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
        /// The constructor for the <c>LiveFitbitDataService</c> class takes an HttpClient and AppSettings as parameters and initialises the class fields and properties.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="settings"></param>
        public LiveFitbitDataService(
            HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;

            AccessToken = new AccessToken();

            AuthorisationUrl = _settings.AuthorisationUrl;
            BaseURL = _settings.BaseURL;
            ClientID = _settings.ClientID ?? string.Empty;
            ClientSecret = _settings.ClientSecret;

            _httpClient.BaseAddress = new Uri(BaseURL);
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
        /// The BuildFitbitAuthUrl method constructs the URL for the Fitbit authorization endpoint 
        /// i.e. the necessary query parameters such as response type, client ID, redirect URI, 
        /// and scope of access. This URL is used to initiate the OAuth2 authorization process with Fitbit.
        /// </summary>
        /// <returns></returns>
        private string BuildFitbitAuthUrl()
        {
            return $"https://www.fitbit.com/oauth2/authorize" +
                   $"?response_type=code" +
                   $"&client_id={ClientID}" +
                   $"&redirect_uri=https://localhost:7039/signin-fitbit" +
                   $"&scope=activity heartrate profile sleep";
        }

        /// <summary>
        /// The AuthCodeFlowAsync method is used to authenticate the user and get an access token from the Fitbit API using the authorization code flow. It takes the authorization code as a parameter, sends a POST request to the Fitbit API to exchange the code for an access token, and deserializes the response into an AccessToken object which is then stored in the AccessToken property of the class.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<bool> AuthCodeFlowAsync(string code)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"https://api.fitbit.com/oauth2/token");

            var authString = $"{ClientID}:{ClientSecret}";
            var authBytes = Encoding.UTF8.GetBytes(authString);
            var authBase64 = Convert.ToBase64String(authBytes);

            request.Headers.Add("Authorization", "Basic " + authBase64);

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", ClientID },
                { "grant_type", "authorization_code" },
                { "redirect_uri", "https://localhost:7039/signin-fitbit" },
                { "code", code }
            });

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            AccessToken = JsonSerializer.Deserialize<AccessToken>(body, _jsonOptions)!;

            return true;
        }

        public Task<bool> HasValidTokenAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FitbitProfile> GetProfileAsync()
        {
            throw new NotImplementedException();
        }

        public Task<HeartRateSummary> GetDailyHeartRateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<List<HeartRateZone>> GetHeartRateZonesAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<Distance> GetDistanceInStepsAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<SleepSummary> GetSleepAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<ActivitySummary> GetDailyActivityAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TestDataConnectionAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<MusicMoodResult> ReadBiometricAndMusicDataAsync()
        {
            var biomStats = await GetDailyHeartRateAsync(DateTime.Now);

            return new MusicMoodResult
            {
                //BiometricSummary = biometric,
                //Recommendations = recommendations
            };
        }

        public Task<BiometricSummary> GetBiometricDataAsync()
        {
            throw new NotImplementedException();
        }

        Task IFitbitDataService.ReadBiometricAndMusicDataAsync()
        {
            return ReadBiometricAndMusicDataAsync();
        }
    }
}
