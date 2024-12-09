using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteV4.Models;

namespace SiteV4.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = 1; // Замените на текущего авторизованного пользователя
            var cartItems = await _context.Cart
                .Include(c => c.Product)
                .Where(c => c.UserID == userId)
                .ToListAsync();

            return View(cartItems);
        }

        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = 1; // Замените на текущего авторизованного пользователя
            var cartItem = await _context.Cart.FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                _context.Cart.Add(new Cart
                {
                    UserID = userId,
                    ProductID = productId,
                    Quantity = 1
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateQuantity(int cartId, int quantity)
        {
            var cartItem = await _context.Cart.FindAsync(cartId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromCart(int cartId)
        {
            var cartItem = await _context.Cart.FindAsync(cartId);
            if (cartItem != null)
            {
                _context.Cart.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
