using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteV4.Models;

namespace SiteV4.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string search, string sortOrder)
        {
            var products = from p in _context.Products select p;

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
            }

            products = sortOrder switch
            {
                "price_asc" => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                _ => products.OrderBy(p => p.Name),
            };

            return View(await products.ToListAsync());
        }
    }
}
