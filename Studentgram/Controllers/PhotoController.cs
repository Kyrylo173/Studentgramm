using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Studentgram.Data;
using Studentgram.Models;

namespace Studentgram.Controllers
{
    public class PhotoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhotoController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, string title, string caption)
        {
            var username = HttpContext.Session.GetString("Username");


            if (file != null && file.Length > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var photo = new Photo
                {
                    Title = title,
                    Caption = caption,
                    Image = "/uploads/" + fileName,
                    Username = username! 
                };

                _context.Photos.Add(photo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return BadRequest("Публікацію не завантажено.");
        }
    }
}
