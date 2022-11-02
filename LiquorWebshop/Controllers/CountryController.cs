using LiquorWebshop.Data;
using LiquorWebshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace LiquorWebshop.Controllers
{
    public class CountryController : Controller
    {
        private readonly DatabaseContext _context;

        public CountryController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries.ToListAsync());
        }

        public async Task<IActionResult> List()
        {
            return View(await _context.Countries.ToListAsync());
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                await _context.Countries.AddAsync(country);
                await _context.SaveChangesAsync();

                TempData["success"] = "Country adde successfully!";

                return RedirectToAction("Index");
            }

            return View(country);
        }

        // GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var country = await _context.Countries.FindAsync(id);

            if (country == null)
                return NotFound();

            return View(country);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                _context.Countries.Update(country);
                await _context.SaveChangesAsync();

                TempData["success"] = "Country edited successfully!";

                return RedirectToAction("Index");
            }

            return View(country);
        }

        // GET
        public async Task<IActionResult> Details(int? id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
                return NotFound();

            return View(country);
        }

        // GET
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    var country = await _context.Countries.FindAsync(id);

        //    if (country == null)
        //        return NotFound();

        //    _context.Countries.Remove(country);
        //    await _context.SaveChangesAsync();

        //    TempData["success"] = "Country deleted successfully!";

        //    return RedirectToAction("Index");
        //}
    }
}
