using Microsoft.Extensions.Options;
using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Application.Interfaces.Fitbit;
using MusicBased_IOT_Platform.Application.Services.Fitbit.Mock;
using MusicBased_IOT_Platform.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MusicBased_IOT_Platform.Application.Services.Fitbit.Live
{
    public class LiveFitbitDataService : IFitbitDataService
    {
        private readonly HttpClient _httpClient;
        private readonly FitbitSettings _settings;
        private readonly IUserRepository _userRepo;
       // private readonly UserSessionService _session;
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
        /// The constructor for the <c>LiveFitbitDataService</c> class takes an HttpClient and AppSettings as parameters and initialises the class fields and properties.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="settings"></param>
        public LiveFitbitDataService(
            HttpClient httpClient, IOptions<FitbitSettings> settings, IUserRepository userRepo, IUserContext userContext)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _userRepo = userRepo;
            _userContext = userContext;
            // _session = session;

            AccessToken = new AccessToken();

            AuthorisationUrl = _settings.AuthorisationUrl;
            BaseURL = _settings.BaseURL;
            ClientID = _settings.ClientID ?? string.Empty;
            ClientSecret = _settings.ClientSecret;

            _httpClient.BaseAddress = new Uri(_settings.BaseURL);
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
        public async Task<bool> AuthCodeFlowAsync(string code, int userID)
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

            var user = await _userRepo.GetByIdAsync(userID);

            if (user != null)
            {
                user.FitbitAccessToken = AccessToken.Token;
                user.FitbitRefreshToken = AccessToken.RefreshToken;
                user.FitbitTokenExpiry =
                    DateTime.UtcNow.AddSeconds(AccessToken.ExpiresIn);

                await _userRepo.UpdateAsync(user);
            }
            return true;
        }

        /// <summary>
        /// The RefreshAccessTokenAsync method is responsible for refreshing the access token when it has expired. 
        /// It sends a POST request to the Fitbit API with the refresh token to obtain a new access token, 
        /// and updates the AccessToken property with the new token information.
        /// </summary>
        /// <returns></returns>
        private async Task RefreshAccessTokenAsync()
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
                { "grant_type", "refresh_token" },
                { "refresh_token", AccessToken.RefreshToken }
            });

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            AccessToken =
                JsonSerializer.Deserialize<AccessToken>(json, _jsonOptions)!;

            AccessToken.DateTimeAcquired = DateTime.UtcNow;

            
            //var user = await _userRepo.GetByIdAsync(_session.CurrentUser!.ID);
            var user = await _userContext.GetCurrentUserAsync();

            if (user != null)
            {
                await _userRepo.GetByIdAsync(user.ID);

                user.FitbitAccessToken = AccessToken.Token;
                user.FitbitRefreshToken = AccessToken.RefreshToken;
                user.FitbitTokenExpiry =
                    DateTime.UtcNow.AddSeconds(AccessToken.ExpiresIn);

                await _userRepo.UpdateAsync(user);
            }
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
        /// The GetAsync method is a helper method that sends an HTTP GET request to the specified endpoint, 
        /// including the access token in the request headers for authentication. 
        /// It then reads the response content as a string and deserializes it into an object of type T 
        /// using the JsonSerializer with the specified options. The deserialized object is returned to the caller.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        private async Task<T> GetAsync<T>(string endpoint)
        {
            if (string.IsNullOrEmpty(AccessToken.Token))
            {
                await LoadTokenFromDatabaseAsync();
            }

            await HasValidTokenAsync();

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

        public async Task LoadTokenFromDatabaseAsync()
        {
            //await _session.LoadUserAsync();

            //var user = _session.CurrentUser;

            var user = await _userContext.GetCurrentUserAsync();

            if (user == null || string.IsNullOrEmpty(user.FitbitAccessToken))
                return;

            AccessToken = new AccessToken
            {
                Token = user.FitbitAccessToken,
                RefreshToken = user.FitbitRefreshToken ?? "",
                ExpiresIn = (int)((user.FitbitTokenExpiry ?? DateTime.UtcNow) - DateTime.UtcNow).TotalSeconds,
                DateTimeAcquired = DateTime.UtcNow
            };
        }

        /// <summary>
        /// The GetProfileAsync method retrieves the user's profile information from the Fitbit API 
        /// by sending a GET request to the appropriate endpoint and deserializing the response 
        /// into a FitbitProfile object which is then returned to the caller.
        /// </summary>
        /// <returns></returns>
        public async Task<FitbitProfile> GetProfileAsync()
        {
            var response = await GetAsync<FitbitProfileResponse>(
                "/1/user/-/profile.json");

            return response.User!;
        }

        /// <summary>
        /// Thw GetDailyHeartRateAsync method retrieves the user's daily heart rate data from the Fitbit API
        /// by sending a GET request to the appropriate endpoint with the specified date and deserializing 
        /// the response into a HeartRateResponse object. 
        /// The method then returns the first HeartRateSummary from the response, which contains information 
        /// about the user's resting heart rate and heart rate zones for that day.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<HeartRateSummary> GetDailyHeartRateAsync(DateTime date)
        {
            string endpoint =
                $"/1/user/-/activities/heart/date/{date:yyyy-MM-dd}/1d.json";

            var response = await GetAsync<HeartRateResponse>(endpoint);

            return response.ActivitiesHeart!.FirstOrDefault()!;
        }

        /// <summary>
        /// THe GetDistanceInStepsAsync method retrieves the user's daily step count data from the Fitbit API by sending a GET request to the appropriate endpoint with the specified date and deserializing the response into a StepsResponse object. The method then returns the first Distance object from the response, which contains information about the user's step count for that day.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<Steps> GetDistanceInStepsAsync(DateTime date)
        {
            string endpoint =
                $"/1/user/-/activities/steps/date/{date:yyyy-MM-dd}/1d.json";

            var response = await GetAsync<StepsResponse>(endpoint);

            return response.ActivitiesSteps!.FirstOrDefault()!;
        }

        /// <summary>
        /// The GetSleepAsync method retrieves the user's sleep data from the Fitbit API by sending a GET request to 
        /// the appropriate endpoint with the specified date and deserializing the response into a SleepResponse object. 
        /// The method then returns the SleepSummary from the response, which contains information about the user's 
        /// sleep duration, quality, and other sleep-related metrics for that day.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<SleepSummary> GetSleepAsync(DateTime date)
        {
            string endpoint =
                $"/1.2/user/-/sleep/date/{date:yyyy-MM-dd}.json";

            var response = await GetAsync<SleepResponse>(endpoint);

            return response.Summary!;
        }

        /// <summary>
        /// The GetDailyActivityAsync method retrieves the user's daily activity data from the Fitbit API by sending a GET request to 
        /// the appropriate endpoint with the specified date and deserializing the response into an ActivityResponse object. 
        /// The method then returns the ActivitySummary from the response, which contains information about the user's step count, 
        /// active minutes, calories burned, and other activity-related metrics for that day.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<ActivitySummary> GetDailyActivityAsync(DateTime date)
        {
            string endpoint =
                $"/1/user/-/activities/date/{date:yyyy-MM-dd}.json";

            var response = await GetAsync<ActivityResponse>(endpoint);

            return response.ActivitiesSummary!;
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
            var biometric = await GetBiometricDataAsync();

            var recommendations = new List<Track>();
            // change the above to be pulled from the spotify live service


            return new MusicMoodResult
            {
                BiometricSummary = biometric,
                RecommendedTracks = recommendations,
                GeneratedAt = DateTime.Now
            };
        }

        public async Task<BiometricSummary> GetBiometricDataAsync()
        {
            Console.WriteLine("LIVE FITBIT SERVICE RUNNING");
            try
            {
                var today = DateTime.Now;

                var heartRate = await GetDailyHeartRateAsync(today);
                var steps = await GetDistanceInStepsAsync(today);
                var activity = await GetDailyActivityAsync(today);
                //var sleep = await GetSleepAsync(today);
                //var breathing = await _piService.GetBreathingRateAsync();

                //var heartRate = await GetDailyHeartRateAsync();
                //var steps = await GetDistanceInStepsAsync();
                //var sleep = await GetSleepAsync();
                //var breathing = await GetBreathingRateAsync();

                return new BiometricSummary
                {
                    AverageRestingHeartRate = heartRate?.RestingHeartRate ?? 0,
                    AverageDailySteps = steps?.Value ?? 0,
                    AverageActiveMinutes = activity?.ActiveMinutes ?? 0,
                    //AverageSleepMinutes = sleep?.TotalMinutesAsleep ?? 0,
                    CapturedAt = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fitbit error: {ex.Message}");
                Console.WriteLine("FALLBACK TO MOCK FITBIT SERVICE");
                Console.WriteLine("Fitbit API unavailable - using mock data");

                // return GetMockBiometricData();
                var mock = new MockFitbitDataService();

                var heartRate = await mock.GetDailyHeartRateAsync(DateTime.Now);
                var activity = await mock.GetDailyActivityAsync(DateTime.Now);

                return new BiometricSummary
                {
                    AverageRestingHeartRate = heartRate?.RestingHeartRate ?? 70,
                    AverageDailySteps = 6500,
                    AverageActiveMinutes = activity?.ActiveMinutes ?? 30,
                    CapturedAt = DateTime.Now
                };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static BiometricSummary GetMockBiometricData()
        {
            return new BiometricSummary
            {
                AverageRestingHeartRate = 68,
                AverageDailySteps = 7500,
                AverageActiveMinutes = 45,
                CapturedAt = DateTime.Now,
                IsCalmState = true
            };
         }
    }
}
