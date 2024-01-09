
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

        public async Task<List<string>> GetVideoThumbnailsAsync(List<string> videoIds)
        {
            // Get account auth token
            var storedAccount = _httpContextAccessor.HttpContext.Session.GetString("Account");
            var account = JsonSerializer.Deserialize<AccountModel>(storedAccount);

            // Send request
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {account.AuthToken}");

            List<string> imgList = new List<string>();

            foreach(var id in videoIds)
            {
                var response = await _httpClient.GetAsync($"{_configuration["AppSettings:Urls:Thumbnails"]}/{id}.jpg");

                if (response.IsSuccessStatusCode)
                {
                    byte[] thumbnailBytes = await response.Content.ReadAsByteArrayAsync();
                    string thumbnail = Convert.ToBase64String(thumbnailBytes);
                    imgList.Add(thumbnail);
                }
                else
                {
                    Console.WriteLine("Failed to get thumbnail");
                    imgList.Add("/Content/defaultThumbnail.jpg"); 
                }
            }

            return imgList;
        }
    }
}
