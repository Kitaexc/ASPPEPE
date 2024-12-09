using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteV4.Models;
using System.Diagnostics;

namespace SiteV4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // �������� 3 ���������� ���������� �� ���� ������
            var featuredCars = await _context.Products.Take(3).ToListAsync();
            return View(featuredCars);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
