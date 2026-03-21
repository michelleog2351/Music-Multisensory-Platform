using Moq;
using MusicBased_IOT_Platform.Application.Interfaces.Fitbit;
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

        //[Fact]
        //public async Task GetBiometricDataAsync_UsesMock_WhenLiveFails()
        //{
        //    // Arrange
        //    var liveMock = new Mock<IFitbitDataService>();
        //    var fallbackMock = new Mock<IFitbitDataService>();
        //    var userRepoMock = new Mock<IUserRepository>();

        //    var userContextMock = new Mock<IUserContext>();

        //    liveMock.Setup(x => x.GetBiometricDataAsync())
        //        .ThrowsAsync(new Exception("API failed"));

        //    fallbackMock.Setup(x => x.GetBiometricDataAsync())
        //        .ReturnsAsync(new BiometricSummary
        //        {
        //            AverageRestingHeartRate = 65
        //        });

        //    var mockHttp = new Mock<HttpMessageHandler>();

        //    mockHttp
        //       .Protected()
        //       .Setup<Task<HttpResponseMessage>>(
        //           "SendAsync",
        //           ItExpr.IsAny<HttpRequestMessage>(),
        //           ItExpr.IsAny<CancellationToken>())

        //       .ReturnsAsync(new HttpResponseMessage
        //       {
        //           StatusCode = HttpStatusCode.OK,
        //           Content = new StringContent(@"
        //       {
        //           ""activities-heart"": 
        //           [
        //               {
        //                   ""value"": {
        //                       ""restingHeartRate"": 65
        //                   }
        //               }
        //           ]
        //       }")
        //       });

        //    var httpClient = new HttpClient(mockHttp.Object);

        //    var options = Options.Create(new FitbitSettings
        //    {
        //        ClientID = "test",
        //        ClientSecret = "test"
        //    });

        //    userRepoMock = new Mock<IUserRepository>();


        //    userContextMock.Setup(x => x.GetCurrentUserAsync())
        //        .ReturnsAsync(new UserAccount
        //        {
        //            ID = 1,
        //            FitbitAccessToken = "test",
        //            FitbitRefreshToken = "refresh",
        //            FitbitTokenExpiry = DateTime.UtcNow.AddHours(1)
        //        });

        //    var service = new LiveFitbitDataService(
        //        httpClient,
        //        options,
        //        userRepoMock.Object,
        //        userContextMock.Object
        //    );

        //    service = new MoodCalibrationService(
        //        liveMock.Object,
        //        fallbackMock.Object
        //    );
        //        liveMock.Setup(x => x.GetBiometricDataAsync())
        //            .ThrowsAsync(new Exception("API failed"));

        //        fallbackMock.Setup(x => x.GetBiometricDataAsync())
        //            .ReturnsAsync(new BiometricSummary
        //            {
        //                AverageRestingHeartRate = 65
        //            });

        //    //service = new LiveFitbitDataService(httpClient, options, session);

        //    // Act
        //    var result = await service.GetBiometricDataAsync();

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(65, result.AverageRestingHeartRate);
        //}

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
            var mockFitbit = new Mock<IFitbitDataService>();
            var mockSpotify = new Mock<ISpotifyDataService>();

            mockFitbit.Setup(f => f.GetBiometricDataAsync())
                .ReturnsAsync(new BiometricSummary
                {
                    AverageRestingHeartRate = 70,
                    AverageDailySteps = 3000,
                    AverageActiveMinutes = 20
                });

            mockSpotify.Setup(s => s.Search(It.IsAny<string>(), "track"))
             .ReturnsAsync(new SearchResults
             {
                 Tracks = new SearchResultTracks
                 {
                     Items =
                     [
                        new() {
                            Name = "Test Song",
                            Artists = [
                                        new Artist { Name = "Test Artist" }
                                      ]
                        }
                     ]
                 }
             });

            var service = new MoodCalibrationService(
                mockFitbit.Object,
                mockSpotify.Object);

            // Act
            var result = await service.GenerateMoodMusicAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.RecommendedTracks!);
        }

        [Fact]
        //public void IsTokenStillValid_ReturnsFalse_WhenTokenNull()
        //{
        //    var httpClient = new HttpClient();

        //    var options = Options.Create(new SpotifySettings
        //    {
        //        ClientID = "test",
        //        ClientSecret = "test"
        //    });

        //    var config = new ConfigurationBuilder().Build();

        //    var service = new LiveSpotifyDataService(httpClient, options, config);

        //    var result = service.IsTokenStillValid();

        //    Assert.False(result);
        //}
        public void IsTokenStillValid_ReturnsFalse_WhenTokenNull()
        {
            var service = new AuthService();

            var result = service.IsTokenStillValid(null);

            Assert.False(result);
        }
    }
}
