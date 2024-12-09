using Microsoft.AspNetCore.Mvc;
using SiteV4.Helpers;
using SiteV4.Models;

namespace SiteV4.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                return Json(new { success = false, message = "Пользователь с такой почтой уже существует." });
            }

            var hashedPassword = PasswordHasher.HashPassword(password);
            var user = new User { Email = email, PasswordHash = hashedPassword };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Регистрация прошла успешно." });
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return Json(new { success = false, message = "Пользователь не найден." });
            }

            var hashedPassword = PasswordHasher.HashPassword(password);
            if (user.PasswordHash != hashedPassword)
            {
                return Json(new { success = false, message = "Неверный пароль." });
            }

            return Json(new { success = true, redirectUrl = Url.Action("Dashboard", "Account") });
        }


    }
}
