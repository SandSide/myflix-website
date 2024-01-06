using myflix_website.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace myflix_website.Services
{
    public class VideoService : IVideoService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VideoService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Video>> GetVideoCatalogueAsync()
        {
            // Get account auth token
            var storedAccount = _httpContextAccessor.HttpContext.Session.GetString("Account");
            var account = JsonSerializer.Deserialize<AccountModel>(storedAccount);

            // Send request
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {account.AuthToken}");
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
            // Get account auth token
            var storedAccount = _httpContextAccessor.HttpContext.Session.GetString("Account");
            var account = JsonSerializer.Deserialize<AccountModel>(storedAccount);

            // Send request
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {account.AuthToken}");
            var response = await _httpClient.GetAsync($"{_configuration["AppSettings:Urls:Video"]}/{filename}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
