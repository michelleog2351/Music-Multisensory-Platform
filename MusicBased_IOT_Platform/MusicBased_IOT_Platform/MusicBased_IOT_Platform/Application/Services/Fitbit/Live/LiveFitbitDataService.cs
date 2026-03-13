using Microsoft.AspNetCore.Connections.Features;
using Microsoft.Extensions.Options;
using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Application.Interfaces.Fitbit;
using MusicBased_IOT_Platform.Models;
using System.Diagnostics;
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
        /// Access token used to access the Fitbit web api
        /// </summary>
        public AccessToken AccessToken { get; set; }

        /// <summary>
        /// AuthorisationUrl used to access the Fitbit web api
        /// </summary>
        public string AuthorisationUrl { get; set; }

        /// <summary>
        /// BaseURL used to access the Fitbit web api
        /// </summary>
        public string BaseURL { get; set; }

        /// <summary>
        /// ClientID used to access the Fitbit web api
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// ClientSecret used to access the Fitbit web api
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

        private async Task<T> GetAsync<T>(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    AccessToken.AccessToken);

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(json, _jsonOptions)!;
        }
        public Task<bool> HasValidTokenAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The 
        /// </summary>
        /// <returns></returns>
        public async Task<FitbitProfile> GetProfileAsync()
        {
            var response = await GetAsync<FitbitProfileResponse>(
                "/1/user/-/profile.json");

            return response.User!;
        }

        public async Task<HeartRateSummary> GetDailyHeartRateAsync(DateTime date)
        {
            string endpoint =
                $"/1/user/-/activities/heart/date/{date:yyyy-MM-dd}/1d.json";

            var response = await GetAsync<HeartRateResponse>(endpoint);

            return response.ActivitiesHeart!.FirstOrDefault()!;
        }

        public async Task<Distance> GetDistanceInStepsAsync(DateTime date)
        {
            string endpoint =
                $"/1/user/-/activities/steps/date/{date:yyyy-MM-dd}/1d.json";

            var response = await GetAsync<StepsResponse>(endpoint);

            return response.ActivitiesSteps!.FirstOrDefault()!;
        }

        public async Task<SleepSummary> GetSleepAsync(DateTime date)
        {
            string endpoint =
                $"/1.2/user/-/sleep/date/{date:yyyy-MM-dd}.json";

            var response = await GetAsync<SleepResponse>(endpoint);

            return response.Summary!;
        }

        public async Task<ActivitySummary> GetDailyActivityAsync(DateTime date)
        {
            string endpoint =
                $"/1/user/-/activities/date/{date:yyyy-MM-dd}.json";

            var response = await GetAsync<ActivityResponse>(endpoint);

            return response.Summary!;
        }

        public Task<List<HeartRateZone>> GetHeartRateZonesAsync(DateTime date)
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

        public async Task<BiometricSummary> GetBiometricDataAsync()
        {
            //var heartRate = await GetDailyHeartRateAsync();
            //var steps = await GetDistanceInStepsAsync();
            //var sleep = await GetSleepAsync();
            //var breathing = await GetBreathingRateAsync();

            return new BiometricSummary
            {
                //RestingHeartRate = heartRate?.RestingHeartRate ?? 0,
                //Steps = int.Parse(steps?.Value ?? "0"),
                //ActiveMinutes = activity?.FairlyActiveMinutes ?? 0,
                //SleepMinutes = sleep?.TotalMinutesAsleep ?? 0,
                //CapturedAt = DateTime.Now
            };
        }

        Task IFitbitDataService.ReadBiometricAndMusicDataAsync()
        {
            return ReadBiometricAndMusicDataAsync();
        }
    }
}
