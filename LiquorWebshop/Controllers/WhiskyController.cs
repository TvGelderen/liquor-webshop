using LiquorWebshop.Data;
using LiquorWebshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiquorWebshop.Controllers
{
    public class WhiskyController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _environment;

        public WhiskyController(DatabaseContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Whiskies.ToListAsync());
        }

        public async Task<IActionResult> List()
        {
            return View(await _context.Whiskies.ToListAsync());
        }

        // GET
        public IActionResult Create()
        {
            ViewData["Countries"] = _context.Countries.ToList();
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Whisky whisky)
        {
            if (whisky == null || whisky.ImageFile == null)
                return View(whisky);

            var imagePath = Path.Combine(_environment.WebRootPath, "img", whisky.ImageFile.FileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await whisky.ImageFile.CopyToAsync(stream);
            }

            whisky.Image = "./img/" + whisky.ImageFile.FileName;

            await _context.Whiskies.AddAsync(whisky);
            await _context.SaveChangesAsync();

            return RedirectToAction("List");
        }
    }
}
