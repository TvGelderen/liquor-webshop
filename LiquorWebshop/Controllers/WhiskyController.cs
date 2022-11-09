using LiquorWebshop.Data;
using LiquorWebshop.Data.Enum;
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

        public async Task<IActionResult> Index(string currentSearchString, string searchString, int countryId, WhiskyType type, WhiskyTaste taste)
        {
            ViewData["Countries"] = _context.Countries.ToList();

            searchString = String.IsNullOrEmpty(searchString) ? currentSearchString : searchString;
            ViewData["CurrentSearchString"] = searchString;

            var whiskies = from whisky in _context.Whiskies
                           select whisky;

            if (!String.IsNullOrEmpty(searchString))
                whiskies = whiskies.Where(whisky => whisky.Name.Contains(searchString));

            if (countryId != 0)
                whiskies = whiskies.Where(whisky => whisky.CountryId == countryId);
            if (type != WhiskyType.All)
                whiskies = whiskies.Where(whisky => whisky.Type == type);
            if (taste != WhiskyTaste.All)
                whiskies = whiskies.Where(whisky => whisky.Taste == taste);

            if (whiskies == null)
                return NotFound();

            return View(whiskies);
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
            if (ModelState.IsValid)
            {
                if (whisky.ImageFile == null)
                    return View(whisky);

                var imagePath = Path.Combine(_environment.WebRootPath, "img", whisky.ImageFile.FileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await whisky.ImageFile.CopyToAsync(stream);
                }

                whisky.Image = "./img/" + whisky.ImageFile.FileName;

                //TESTING
                whisky.CountryId = 1;

                await _context.Whiskies.AddAsync(whisky);
                await _context.SaveChangesAsync();

                return RedirectToAction("List");
            }

            Console.WriteLine(ModelState);

            return View(whisky);
        }
    }
}
