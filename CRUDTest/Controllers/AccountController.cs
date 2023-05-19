using Microsoft.AspNetCore.Mvc;

namespace CRUDTest.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
