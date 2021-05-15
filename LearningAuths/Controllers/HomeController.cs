using LearningAuths.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LearningAuths.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Denied()
        {
            return View();
        }
        [Authorize]
        public IActionResult Private()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "testEmail"),
                new Claim("FullName", "testName"),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var props = new AuthenticationProperties() {
                ExpiresUtc = DateTime.UtcNow.AddSeconds(20)
            };
            HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    props).Wait();
            return View();
        }
    }
}
