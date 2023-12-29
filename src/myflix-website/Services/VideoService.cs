using myflix_website.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace myflix_website.Services
{
    public class VideoService : IVideoService
    {
        private readonly HttpClient _httpClient;

        public VideoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Video>> GetVideoCatalogueAsync()
        {
            var response = await _httpClient.GetAsync("http://35.209.185.57/api/Videos");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var videos = JsonSerializer.Deserialize<List<Video>>(jsonString, options);

            return videos;
        }
    }
}
