using CRUDTest.Data;
using CRUDTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDTest.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly DBContext _context;

        public RegistrationController(DBContext context)
        {
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Users user) 
        {   
            try
            {
                using (_context)
                {
                    var userData = new Users()
                    {
                        Email = user.Email.ToLower().Trim(),
                        Name = user.Name,
                        Password = user.Password
                    };

                    await _context.Users.AddAsync(userData);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Login","Access");
            }
            catch (Exception ex)
            {
                ViewData["ValidateMessage"] = $"Error: {ex.Message}";
                return View();
            }
        }
    }
}
