using AddToCart.Data;
using AddToCart.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddToCart.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
           this._context=context;
        }
        public IActionResult Index()
        {
            var products=_context.products.ToList();
            return View(products);
        }
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult create(Product p)
        {
            if (ModelState.IsValid)
            {
              _context.products.Add(p);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}
