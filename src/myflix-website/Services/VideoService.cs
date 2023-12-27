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

        public async Task<List<Video>> GetVideoAsync()
        {
            var response = await _httpClient.GetAsync("http://http://35.184.60.220/api/Videos");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            var videos = JsonSerializer.Deserialize<List<Video>>(jsonString);

            return videos;
        }
    }
}
