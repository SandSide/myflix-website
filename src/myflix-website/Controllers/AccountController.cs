using Microsoft.AspNetCore.Mvc;
using myflix_website.Enums;
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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterModel registerModel)
        {
            var result = await _authService.RegisterAsync(registerModel);

            if(result == OperationResult.Success) 
            {
                TempData["SuccessMessage"] = "Registration was successful. You can now log in.";
                return View("Login");
            }
            else
            {
                ModelState.AddModelError("", "Registration failed. Please check your information and try again.");
                return View();
            }
        }
    }
}
