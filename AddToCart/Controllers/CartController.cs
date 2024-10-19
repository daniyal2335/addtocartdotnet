using AddToCart.Data;
using AddToCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AddToCart.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var prod = _context.cartItems.Include(x => x.product).ToList();
            return View(prod);
        }
        public IActionResult AddToCart(int id)
        {
            var products = _context.products.Find(id);
            if (products == null)
            {
                return NotFound();
            }
            var cartItems=_context.cartItems.SingleOrDefault(x=>x.productId == id);
            if (cartItems == null)
            {
                cartItems = new CartItem()
                {
                    productId = id,
                    product = products,
                    quantity = 1
                };
                _context.cartItems.Add(cartItems);

            }
            else
            {
                cartItems.quantity++;
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(int id) {
            var cartItem = _context.cartItems.SingleOrDefault(x => x.productId == id);
            if (cartItem != null)
            {
                _context.cartItems.Remove(cartItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        
        }
		// Update item quantity
		[HttpPost]
		public IActionResult UpdateQuantity(int id, string action)
		{
			var cartItem = _context.cartItems.Include(x => x.product).SingleOrDefault(x => x.productId == id);
			if (cartItem == null)
			{
				return NotFound();
			}

			if (action == "increase")
			{
				cartItem.quantity++;
			}
			else if (action == "decrease" && cartItem.quantity > 1)
			{
				cartItem.quantity--;
			}

			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
