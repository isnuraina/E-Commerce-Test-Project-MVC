using E_Commerce_Test_Project_MVC.Areas.Admin.ViewModels.Category;
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
        [HttpGet]
        public async Task<IActionResult> Edit(int?id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var existSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            if (existSlider is null)
            {
                return NotFound();
            }
            return View(new SliderEditVM { ImagePath=existSlider.Img});        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM slider)
        {
            if (id is null) return BadRequest();

            var existSlider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            if (existSlider is null) return NotFound();

            if (!ModelState.IsValid)
            {
                slider.ImagePath = existSlider.Img; 
                return View(slider);
            }

            if (slider.Image != null)
            {
                if (!slider.Image.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("Image", "Yalnız şəkil faylları yüklənə bilər");
                    slider.ImagePath = existSlider.Img;
                    return View(slider);
                }

                if (slider.Image.Length / 1024 > 100)
                {
                    ModelState.AddModelError("Image", "Şəklin ölçüsü maksimum 100KB olmalıdır");
                    slider.ImagePath = existSlider.Img;
                    return View(slider);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(slider.Image.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await slider.Image.CopyToAsync(stream);
                }

                string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", existSlider.Img);

                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                existSlider.Img = fileName;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
