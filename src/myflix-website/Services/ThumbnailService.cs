
using myflix_website.Models;
using System.Text.Json;

namespace myflix_website.Services
{
    public class ThumbnailService : IThumbnailService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ThumbnailService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetVideoThumbnailAsync(string videoId)
        {
            // Get account auth token
            var storedAccount = _httpContextAccessor.HttpContext.Session.GetString("Account");
            var account = JsonSerializer.Deserialize<AccountModel>(storedAccount);

            // Send request
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {account.AuthToken}");
            var response = await _httpClient.GetAsync($"{_configuration["AppSettings:Urls:Thumbnails"]}/{videoId}.jpg");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }
        }
    }
}
