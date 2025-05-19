using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studentgram.Data;
using Studentgram.Models;
using System.Diagnostics;

namespace Studentgram.Controllers
{
    public class HomeController : Controller
    { 
        private readonly ApplicationDbContext _context;
       public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var photos = _context.Photos.Include(p => p.User).Include(p => p.Comments).ThenInclude(c => c.User).ToList();
            return View(photos);

        }

        [HttpGet]
        public IActionResult AddComment(int photoId)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == photoId);
            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }

        [HttpPost]
        public IActionResult AddComment(int photoId, string content)
        {
            string username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized();
            }

            var comment = new Commentar
            {
                Content = content,
                Created = DateTime.UtcNow,
                Username = username, //Зв'язуємо користувача з коментарем 
                PhotoId = photoId
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Like(int photoId)
        {
            string username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Auth");
            }
            var photo = _context.Photos.FirstOrDefault(p => p.Id == photoId);
            if (photo == null)
            {
                return NotFound();
            }

            var likedUsers = new HashSet<string>(photo.LikedUsers);
            if (likedUsers.Contains(username))
            {
                likedUsers.Remove(username);
                photo.Like--;
            }
            else
            {
                likedUsers.Add(username);
                photo.Like++;
            }
            photo.LikedUsers  = likedUsers;
            _context.Photos.Update(photo);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
