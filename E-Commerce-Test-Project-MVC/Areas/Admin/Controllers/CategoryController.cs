using E_Commerce_Test_Project_MVC.Data;
using E_Commerce_Test_Project_MVC.Models;
using E_Commerce_Test_Project_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce_Test_Project_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;

        public CategoryController(ICategoryService categoryService, AppDbContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //return View(await _categoryService.GetAllAsync());
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            bool existCategory = await _context.Categories.AnyAsync(m => m.Name.Trim() == category.Name.Trim());
            if (existCategory)
            {
                ModelState.AddModelError("Name", "Category already exist !");
                return View();
            }
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult>Detail(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var existCategory = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (existCategory is null)
            {
                return NotFound();
            }
            return View(existCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Category category)
        {
            if (id is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            var existCategory = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (existCategory is null)
            {
                return NotFound();
            }
            bool existCategoryCheck = await _context.Categories.AnyAsync(m => m.Name.Trim() == category.Name.Trim());
            if (existCategoryCheck)
            {
                ModelState.AddModelError("Name", "Category already exist !");
                return View();
            }
            existCategory.Name = category.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
