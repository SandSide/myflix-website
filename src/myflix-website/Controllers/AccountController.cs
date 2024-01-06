using Microsoft.AspNetCore.Mvc;
using myflix_website.Models;
using myflix_website.Services;
using System.Net.Http;
using System.Security.Principal;
using System.Text.Json;

namespace myflix_website.Controllers
{
    public class AccountController : Controller
    {
        public readonly IAuthService _authService;

        public AccountController(IAuthService authService) 
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await _authService.Login(loginModel);

            if(result != null)
            {
                // Store logged in account details in session
                var serializedAccount = JsonSerializer.Serialize(result);
                HttpContext.Session.SetString("Account", serializedAccount);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}
