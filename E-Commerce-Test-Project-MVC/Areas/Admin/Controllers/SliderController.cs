using E_Commerce_Test_Project_MVC.Areas.Admin.ViewModels.Slider;
using E_Commerce_Test_Project_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Test_Project_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
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

            if (request.Image.Length/1024>100)
            {
                ModelState.AddModelError("Image", "Image size must be max 100KB");
                return View(request);
            }

            if (!request.Image.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "Image type must be image format");
                return View(request);
            }

            return View();
        }
    }
}
