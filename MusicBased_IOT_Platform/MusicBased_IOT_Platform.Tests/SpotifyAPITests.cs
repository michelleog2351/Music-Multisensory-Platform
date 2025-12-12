namespace MusicBased_IOT_Platform.Tests
{
    public class SpotifyApiTests
    {
        [Fact]

        // Test for getting tracks from Spotify API
        public void GetTracks()
        {
            // Arrange
            var spotifyApi = new SpotifyApi();
            var expectedTrackCount = 10;

            // Act
            var tracks = spotifyApi.GetTracks("artist-id", expectedTrackCount);
            // Assert
            Assert.NotNull(tracks);
            Assert.Equal(expectedTrackCount, tracks.Count);
        }
    }
}
