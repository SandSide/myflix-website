using myflix_website.Models;

namespace myflix_website.Services
{
    public interface IAuthService
    {
        Task<string> Login(LoginModel loginModel);

    }
}
