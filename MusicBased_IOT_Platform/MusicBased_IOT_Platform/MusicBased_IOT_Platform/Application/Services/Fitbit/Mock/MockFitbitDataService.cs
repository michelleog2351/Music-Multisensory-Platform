using MusicBased_IOT_Platform.Application.Interfaces.Fitbit;
using MusicBased_IOT_Platform.Models;
using System.Text.Json;

namespace MusicBased_IOT_Platform.Application.Services.Fitbit.Mock
{
    public class MockFitbitDataService : IFitbitDataService
    {

        private readonly string _jsonFilePath;

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
        /// The MockFitbitDataService constructor for the MockFitbitDataService class initialises the file path to the mock JSON data.
        /// </summary>
        public MockFitbitDataService()
        {
            _jsonFilePath = Path.Combine(
                AppContext.BaseDirectory,
                "Application",
                "Services",
                "Fitbit",
                "Mock",
                "TestData"
                );
        }

        public Task<bool> AuthCodeFlowAsync(string code, int userID)
        {
            throw new NotImplementedException();
        }

        public BreathingRate GetBreathingRateData()
        {
            var filePath = Path.Combine(_jsonFilePath, "breathing_rate.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<BreathingRate>(
                jsonData, _jsonOptions)!;
        }

        public Task<ActivitySummary> GetDailyActivityAsync(DateTime date)
        {
            var activity = new ActivitySummary
            {
                ActiveMinutes = 30
            };

            return Task.FromResult(activity);
        }

        public Task<HeartRateSummary> GetDailyHeartRateAsync(DateTime date)
        {
            var filePath = Path.Combine(_jsonFilePath, "heart_rate.json");

            var jsonData = File.ReadAllText(filePath);

            var result = JsonSerializer.Deserialize<HeartRateSummary>(
                jsonData,
                _jsonOptions
            )!;

            return Task.FromResult(result);
        }

        public Task<Steps> GetDistanceInStepsAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<List<HeartRateZone>> GetHeartRateZonesAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<FitbitProfile> GetProfileAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SleepSummary> GetSleepAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasValidTokenAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> TestDataConnectionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BiometricSummary> GetBiometricDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MusicMoodResult> ReadBiometricAndMusicDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}
