using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studentgram.Data;
using System.Net.WebSockets;

namespace Studentgram.Controllers
{

    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Profile()
        {

            string username = HttpContext.Session.GetString("Username");
           
            var user = _context.User.FirstOrDefault(u => u.Username == username);
            
            var imageuser = _context.User
                .Include(u => u.Photos)
                .FirstOrDefault(u => u.Username == username);
           
            if(user == null)
            {
                return NotFound();
            }
            else
            {
                return View("Profile", user);
            }
           
        
        }
        [HttpPost]
        public IActionResult AddDescription(string description)
        {
            // Получаем имя пользователя из сессии
            string username = HttpContext.Session.GetString("Username");
            var user = _context.User.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            if(string.IsNullOrEmpty(user.Description)) 
            {
                user.Description = description;
            }
            else
            {
                user.Description = description;
            }  

            _context.SaveChanges();

            return RedirectToAction("Profile");  
        }
        
        
        public IActionResult ProfileSettings()
        {
            string username = HttpContext.Session.GetString("Username");
            var user = _context.User.FirstOrDefault(s => s.Username == username);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult ProfileSettings(string NewDescription)
        {
            string username = HttpContext.Session.GetString("Username");
            var user = _context.User.FirstOrDefault(s => s.Username == username);

            if (user == null)
            {
                return NotFound();
            }
            
            user.Description = NewDescription;
            _context.SaveChanges();
            
         return RedirectToAction("Profile");
        }

        [Route("profile")]
        [HttpGet("{username}")]
        public IActionResult UserProfile(string username)
        {
            var user = _context.User
             .Include(u => u.Photos)
             .FirstOrDefault(u => u.Username == username);
          
            return View(user);
        }
    }
}
