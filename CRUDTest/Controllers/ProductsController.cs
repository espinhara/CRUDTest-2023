using CRUDTest.Data;
using CRUDTest.Interfaces;
using CRUDTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDTest.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly DBContext _dbContext;
        public ProductsController(DBContext dbContext, IPhotoService photo) { 
            _dbContext = dbContext;
            _photoService = photo;
        }
        // GET: ProductsController
        public ActionResult Index()
        {
            using (_dbContext)
            {
                ViewModelProducts viewModelProducts = new ViewModelProducts();
                 viewModelProducts.ProductsList=  _dbContext.Products
                    .OrderBy(p => p.Name)
                    .ThenBy(p => p.Description)
                    .ThenBy(p => p.Value)
                    .ThenBy(p => p.Image)
                    .ToArray()
                    .ToList();

                return View(viewModelProducts);
            }
        }
        public JsonResult List()
        {
            using (_dbContext)
            {
                List<Products> products = new List<Products>();
                products = _dbContext.Products
                   .OrderBy(p => p.Name)
                   .ThenBy(p => p.Description)
                   .ThenBy(p => p.Value)
                   .ThenBy(p => p.Image)
                   .ToArray()
                   .ToList();

                return Json(products);
            }
        }
        public ActionResult Management()
        {
            using (_dbContext)
            {
                ViewModelProducts viewModelProducts = new ViewModelProducts();
                 viewModelProducts.ProductsList=  _dbContext.Products
                    .OrderBy(p => p.Name)
                    .ThenBy(p => p.Description)
                    .ThenBy(p => p.Value)
                    .ThenBy(p => p.Image)
                    .ToArray()
                    .ToList();

                return View(viewModelProducts);
            }
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        public async Task<IActionResult> Create(ViewModelProducts collection)
        {
            try
            {
                var result = await _photoService.AddPhotoAsync(collection.Image);
                var products = new Products
                {
                    Created = DateTime.Now,
                    Description = collection.Description,
                    Image = result.Url.ToString(),
                    Name = collection.Name,
                    Value = double.Parse(collection.Value)
                };

                using(_dbContext)
                {
                    await _dbContext.Products.AddAsync(products);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ViewData["ValidateMessage"] = "Ocorreu um erro ao tenatar criar um novo registro na base de dados";
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        
        public ActionResult Edit(int id)
        {
            using (_dbContext)
            {
                var product = _dbContext.Products.FirstOrDefault(x => x.Id == id);
                if(product != null)
                {
                    return View(product);
                }
                else
                {
                    return RedirectToAction("Management");
                }
            }
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(ViewModelProducts collection)
        {
            try
            {
                using (_dbContext)
                {
                    var product = _dbContext.Products.FirstOrDefault(x => x.Id ==collection.Id);
                    if (product != null)
                    {
                        string url = null;
                        if (collection.Image != null)
                        {
                            var result = await _photoService.AddPhotoAsync(collection.Image);
                            url = result.Url.ToString();
                        }

                        product.Updated = DateTime.Now;
                        product.Name = collection.Name;
                        product.Description = collection.Description;
                        product.Image = url ?? product.Image;
                        product.Value = double.Parse(collection.Value);
                        product.Created = product.Created;

                        _dbContext.Products.Update(product);
                        await _dbContext.SaveChangesAsync();

                        return RedirectToAction(nameof(Management));
                    }
                    else
                    {
                        ViewData["ValidateMessage"] = "Id do Produto não encontrado na base de dados";
                        return View();
                    }
                }

            }
            catch
            {
                ViewData["ValidateMessage"] = "Ocorreu um erro ao tentar editar o produto";
                return View();
            }
        }


        // POST: ProductsController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (_dbContext)
                {
                    var product = await _dbContext.Products.FindAsync(id);
                    if (product != null)
                    {
                        _dbContext.Products.Remove(product);
                        await _dbContext.SaveChangesAsync();

                        return RedirectToAction(nameof(Management));
                    }
                    else
                    {
                        ViewData["ValidateMessage"] = "Id do Produto não encontrado na base de dados";
                        return View();
                    }
                }
            }
            catch
            {
                ViewData["ValidateMessage"] = "Ocorreu um erro ao tentar excluir o produto";
                return View();
            }
        }
    }
}
