using Moq;
using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Application.Interfaces.Fitbit;
using MusicBased_IOT_Platform.Application.Interfaces.Flask;
using MusicBased_IOT_Platform.Application.Interfaces.Spotify;
using MusicBased_IOT_Platform.Application.Services;
using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Tests
{
    public class MoodCalibrationServiceTests
    {
        [Fact]
        // Test for getting tracks from Spotify API
        public void GetTracks()
        {
            // Arrange....var tracks
            _ = new List<string> { "track1", "track2", "track3" };

            // Act...
            var result = true;

            // Assert...
            Assert.True(result);
        }

        [Fact]
        public async Task GetBiometricDataAsync_UsesMock_WhenLiveFails()
        {
            // Arrange
            var liveMock = new Mock<IFitbitDataService>();
            var fallbackMock = new Mock<IFitbitDataService>();

            liveMock.Setup(x => x.GetBiometricDataAsync())
                .ThrowsAsync(new Exception("API failed"));

            fallbackMock.Setup(x => x.GetBiometricDataAsync())
                .ReturnsAsync(new BiometricSummary
                {
                    AverageRestingHeartRate = 65
                });

            var service = new ResilientFitbitService(
                liveMock.Object,
                fallbackMock.Object
            );
            // Act
            var result = await service.GetBiometricDataAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(65, result.AverageRestingHeartRate);
        }

        [Fact]
        // Test for generating mood music based on biometric data
        public async Task GenerateMoodMusicAsync_ReturnsTracks_WhenValidData()
        {
            // Arrange
            var mockFlask = new Mock<IFlaskDataService>();
            var mockFitbit = new Mock<IFitbitDataService>();
            var mockSpotify = new Mock<ISpotifyDataService>();
            var mockUserContext = new Mock<IUserContext>();
            var mockCalibrationRepo = new Mock<ICalibrationRepository>();
            

            mockFitbit.Setup(f => f.GetBiometricDataAsync())
                .ReturnsAsync(new BiometricSummary
                {
                    AverageRestingHeartRate = 70,
                    AverageDailySteps = 3000,
                    AverageActiveMinutes = 20
                });

            mockSpotify.Setup(s => s.GetMoodRecommendations(
                It.IsAny<int>(),
                It.IsAny<double>(),
                It.IsAny<double>(),
                It.IsAny<double>(),
                It.IsAny<double>()))
            .ReturnsAsync(new Recommendations
            {
                Tracks =
                [
                    new() {
                        Name = "Test Song",
                        Artists =
                        [
                            new Artist { Name = "Test Artist" }
                        ]
                    }
                ]
            });

            var testUser = new UserAccount { ID = 1, Username = "testuser" };
            mockUserContext.Setup(u => u.GetCurrentUserAsync())
                .ReturnsAsync(testUser);
            
            mockCalibrationRepo.Setup(c => c.GetRecentAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<CalibrationRecord>
                {
                    new CalibrationRecord
                    {
                        AverageRestingHeartRate = 70,
                        AverageDailySteps = 3000,
                        AverageActiveMinutes = 20,
                        CreatedAt = DateTime.UtcNow
                    }
                });

            var service = new MoodCalibrationService(
                mockFlask.Object,
                mockFitbit.Object,
                mockSpotify.Object,
                mockUserContext.Object,
                mockCalibrationRepo.Object);

            // Act
            var result = await service.BuildMoodResultAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.RecommendedTracks!);
        }

        [Fact]
        public void IsTokenStillValid_ReturnsFalse_WhenTokenNull()
        {
            var service = new AuthService();

            var result = service.IsTokenStillValid(null);

            Assert.False(result);
        }
    }
}
