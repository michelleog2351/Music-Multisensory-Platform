using MusicBased_IOT_Platform.Application.Interfaces.Flask;
using MusicBased_IOT_Platform.Models;
using System.Net.Http.Headers;

namespace MusicBased_IOT_Platform.Application.Services.Flask
{
    public class FlaskDataService : IFlaskDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IUserContext _userContext;

        public FlaskDataService(HttpClient httpClient, IUserContext userContext)
        {
            _httpClient = httpClient;
            _userContext = userContext;
        }

        public async Task<BiometricSummary> GetBiometricDataAsync()
        {
            var user = await _userContext.GetCurrentUserAsync();

            if (user == null || string.IsNullOrEmpty(user.FitbitAccessToken))

                throw new InvalidOperationException("User not authenticated with Fitbit");
            
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://192.168.1.11:5000/api/biometric");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.FitbitAccessToken);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<BiometricSummary>();

            return result ?? new BiometricSummary();
        }
    }
}
