using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studentgram.Models;
using Studentgram.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Text;
using System.Security.Cryptography;
namespace Studentgram.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Fill all blanks");
                return View();
            }
            if (await _context.User.AnyAsync(u => u.Username == username)) {
                ModelState.AddModelError("", "Такий користувач вже є");
                return View();
            }
            var passwordHash = HashPassword(password);

            var user = new Users { Username = username, HashPassword = passwordHash };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !VerifyPassword(password, user.HashPassword))
            {
                ModelState.AddModelError("", "Неправильний логін або пароль!");
                return View();
            }


            HttpContext.Session.SetString("Username", username);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login");
        }

private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
