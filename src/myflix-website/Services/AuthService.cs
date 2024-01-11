using Microsoft.AspNetCore.Mvc;
using myflix_website.Enums;
using myflix_website.Models;
using System.Text;
using System.Text.Json;

namespace myflix_website.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AccountModel> Login(LoginModel loginModel)
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
                var account = new AccountModel { Username = loginModel.Username, Password = loginModel.Password, AuthToken = token };
                Console.WriteLine($"Token received: {token}");

                return account;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }
        }

        public async Task<OperationResult> LogoutAsync()
        {
            if(_httpContextAccessor.HttpContext.Session.Get("Account") != null)
            {
                _httpContextAccessor.HttpContext.Session.Remove("Account");
                return OperationResult.Success;
            }

            return OperationResult.Failure;
        }

        public async Task<OperationResult> RegisterAsync(RegisterModel registerModel)
        {
            // Create request
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(registerModel),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["AppSettings:Urls:Auth"]}/register", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Success;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return OperationResult.Failure;
            }
        }
    }
}
