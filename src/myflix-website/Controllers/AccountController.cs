using Microsoft.AspNetCore.Mvc;
using myflix_website.Models;
using myflix_website.Services;
using System.Net.Http;

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
            //var result = await _authService.Login("hello", "world");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var result = await _authService.Login(loginModel);

            if(result != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

        }
    }
}
