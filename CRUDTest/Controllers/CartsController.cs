using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUDTest.Data;
using System.Net.Security;
using Microsoft.AspNetCore.Authentication;
using CRUDTest.Models;
using Newtonsoft.Json;

namespace CRUDTest.Controllers
{
    public class CartsController : Controller
    {
        private readonly DBContext _dbContext;

        public CartsController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: CartsController
        public async Task<IActionResult> Index()
        {
            using (_dbContext)
            {
                var user = HttpContext.User.Claims.ToList()[0].Value;
                if(user != null)
                {
                    var userDB = _dbContext.Users.FirstOrDefault(u => u.Name == user);
                    if(userDB != null)
                    {
                        List<Carts> carts = _dbContext.Carts
                               .Join(_dbContext.Products, c => c.ProductId, p => p.Id, (c, p) => new { Carts = c, Products = p })
                               .Where(cp => cp.Carts.UserId == userDB.Id)
                               .Select(cp => cp.Carts)
                               .ToList();

                        return Json(carts);
                    }
                }
            }

            return Json(new {});
        }

        // GET: CartsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartsController/Create
        [HttpPost]
        public async  Task<IActionResult> Create(int id)
        {
            try
            {
                using (_dbContext)
                {
                    var user = HttpContext.User.Claims.ToList()[0].Value;
                    if (user != null)
                    {
                        var userDB = _dbContext.Users.FirstOrDefault(u => u.Name == user);
                        if (userDB != null)
                        {
                            var cart = new Carts
                            {
                                ProductId = id,
                                UserId = userDB.Id,
                            };
                            await _dbContext.Carts.AddAsync(cart);
                            await _dbContext.SaveChangesAsync();

                            return View(cart);
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: CartsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
