using myflix_website.Models;
using System.Text;
using System.Text.Json;

namespace myflix_website.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<String> Login(LoginModel loginModel)
        {
            // Create request
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(loginModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["AppSettings:Urls:Auth"]}/login", jsonContent);

            if(response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Token received: {token}");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }

            return null;
            
        }
    }
}
