using Microsoft.AspNetCore.Connections.Features;
using MusicBased_IOT_Platform.Application.Interfaces.Fitbit;
using MusicBased_IOT_Platform.Models;
using System.Text.Json;

namespace MusicBased_IOT_Platform.Application.Services.Fitbit.Mock
{
    public class MockFitbitDataService : IFitbitService
    {
        
        private readonly string _jsonFilePath;

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

        public BreathingRate GetBreathingRateData()
        {
            var filePath = Path.Combine(_jsonFilePath, "heart.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} was not found.");
            }

            var jsonData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<BreathingRate>(
                jsonData,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
        }

        public IConnectionHeartbeatFeature GetHeartbeatFeature()
        {
            throw new NotImplementedException();
        }

        public bool TestDataConnection()
        {
            throw new NotImplementedException();
        }
    }
}
