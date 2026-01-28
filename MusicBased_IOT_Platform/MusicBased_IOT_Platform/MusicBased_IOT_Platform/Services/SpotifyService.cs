using MusicBased_IOT_Platform.Models;
using System.Text.Json;

namespace MusicBased_IOT_Platform.Services
{
    public class SpotifyService
    {
        private readonly HttpClient _http;

        public SpotifyService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Album> GetAlbumAsync(string albumId)
        {
            var response = await _http.GetAsync($"https://api.spotify.com/v1/albums/{albumId}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Album>(json);
        }
    }

}


