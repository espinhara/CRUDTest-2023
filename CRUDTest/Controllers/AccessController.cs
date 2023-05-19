using CRUDTest.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using CRUDTest.Data;

namespace CRUDTest.Controllers
{
    public class AccessController : Controller
    {
        private readonly DBContext _context;

        public AccessController( DBContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;

            if (claimsUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Products");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(ViewModelLogin modelLogin)
        {
            using (_context)
            {
                var user = _context.Users.FirstOrDefault(u => u.Name == modelLogin.UserName.ToLower().Trim());
                if (user == null || user.Password != modelLogin.Password)
                {
                    ViewData["ValidateMessage"] = "User not found or Password incorrect";

                    return View();
                }
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.UserName),
                    new Claim("OtherProperties", "Example Role")
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLogedIn,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    properties
                );

                return RedirectToAction("Index", "Products");
            }
        }

    }
}
