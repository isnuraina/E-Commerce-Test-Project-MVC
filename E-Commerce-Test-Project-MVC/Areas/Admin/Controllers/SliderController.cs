using E_Commerce_Test_Project_MVC.Areas.Admin.ViewModels.Slider;
using E_Commerce_Test_Project_MVC.Data;
using E_Commerce_Test_Project_MVC.Helpers.Extensions;
using E_Commerce_Test_Project_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Test_Project_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task< IActionResult> Index()
        {
            var sliders = await _context.Sliders.Select(m => new SliderVM {Id=m.Id, Image = m.Img }).ToListAsync();
            return View(sliders);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            if (slider is null)
            {
                return NotFound();
            }
            return View(new SliderDetailVM {Img=slider.Img});
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            if (request.Image.CheckFileSize(400))
            {
                ModelState.AddModelError("Image", "Image size must be max 400KB");
                return View(request);
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Image type must be image format");
                return View(request);
            }
            string fileName = Guid.NewGuid().ToString()+ "-" + request.Image.FileName;
            string path = Path.Combine(_env.WebRootPath,"assets", "img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }
            await _context.Sliders.AddAsync(new Slider { Img = fileName });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            if (slider is null)
            {
                return NotFound();
            }
            string path = Path.Combine(_env.WebRootPath, "assets", "img", slider.Img);
            path.Delete();
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
