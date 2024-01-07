using myflix_website.Enums;
using myflix_website.Models;

namespace myflix_website.Services
{
    public interface IAuthService
    {
        Task<AccountModel> Login(LoginModel loginModel);
        Task<OperationResult> RegisterAsync(RegisterModel registerModel);
    }
}
