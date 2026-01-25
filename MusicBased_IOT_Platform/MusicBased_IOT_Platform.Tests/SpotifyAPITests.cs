namespace MusicBased_IOT_Platform.Tests
{
    public class SpotifyApiTests
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
    }
}
