using Microsoft.AspNetCore.Connections.Features;
using Microsoft.Extensions.Options;
using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Application.Interfaces.Fitbit;
using MusicBased_IOT_Platform.Models;
using System.Text;
using System.Text.Json;

namespace MusicBased_IOT_Platform.Application.Services.Fitbit.Live
{
    public class LiveFitbitDataService : IFitbitService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;

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
        /// The <c>AuthoriseClientAsync</c> method authorises the client to access the spotify web API and gets an access token
        /// </summary>
        /// <returns>A <c>bool</c> Authorisation has been granted.</returns>
        public async Task<bool> AuthoriseClientAsync()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{AuthorisationUrl}?grant_type=client_credentials");

            // The authorisation string consisting of the ClientID and ClientSecret has to be
            // converted into a Base64 string for the Spotify authorisation request. 
            string auth_string = $"{ClientID}:{ClientSecret}";
            byte[] auth_bytes = Encoding.UTF8.GetBytes(auth_string);
            string auth_base64 = Convert.ToBase64String(auth_bytes);

            // Add the header information
            request.Headers.Add("Authorization", "Basic " + auth_base64);

            request.Content = new StringContent(
                string.Empty,
                Encoding.UTF8,
                "application/x-www-form-urlencoded");

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
                // https://developer.spotify.com/documentation/web-api/concepts/api-calls
                AccessToken = new AccessToken
                {
                    Token = "unable to acquire token"
                };
                return false;
            }

            // Read the response body as a string
            string responseBody = await response.Content.ReadAsStringAsync();

            // Deserialise the JSON response into an AccessToken object
            // For details on deserialising JSON see:
            // https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/deserialization

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            AccessToken =
                JsonSerializer.Deserialize<AccessToken>(responseBody, options)!;
            return true;
        }



        public IConnectionHeartbeatFeature GetHeartbeatFeature()
        {
            // Implement logic to retrieve heartbeat feature from Fitbit API
            throw new NotImplementedException();
        }

        public bool TestDataConnection()
        {
            throw new NotImplementedException();
        }

        IConnectionHeartbeatFeature IFitbitService.GetHeartbeatFeature()
        {
            throw new NotImplementedException();
        }

        //   public HeartRateSummary GetDailyHeartRate(DateTime date)


    }
}
