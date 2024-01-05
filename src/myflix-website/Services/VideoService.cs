using myflix_website.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace myflix_website.Services
{
    public class VideoService : IVideoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public VideoService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<Video>> GetVideoCatalogueAsync()
        {
            var response = await _httpClient.GetAsync(_configuration["AppSettings:Urls:VideoCatalogue"]);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var videos = JsonSerializer.Deserialize<List<Video>>(jsonString, options);

            return videos;
        }

        public async Task<byte[]> GetVideoFromUrlAsync(string filename)
        {
            var response = await _httpClient.GetAsync($"{_configuration["AppSettings:Urls:Video"]}/{filename}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
