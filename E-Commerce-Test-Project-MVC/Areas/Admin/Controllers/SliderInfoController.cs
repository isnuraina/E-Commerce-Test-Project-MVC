using Azure.Core;
using E_Commerce_Test_Project_MVC.Areas.Admin.ViewModels.Slider;
using E_Commerce_Test_Project_MVC.Areas.Admin.ViewModels.SliderInfo;
using E_Commerce_Test_Project_MVC.Data;
using E_Commerce_Test_Project_MVC.Helpers.Extensions;
using E_Commerce_Test_Project_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Test_Project_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SliderInfoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderInfoController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var sliderInfo = await _context.SliderInfos.AsNoTracking().ToListAsync();
            return View(sliderInfo);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var sliderInfo = await _context.SliderInfos.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (sliderInfo is null)
            {
                return NotFound();
            }
            return View(sliderInfo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(SliderInfoCreateVM sliderInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(sliderInfo);
            }
            string fileName = Guid.NewGuid().ToString() + "-" + sliderInfo.Image.FileName;
            string path = Path.Combine(_env.WebRootPath, "assets", "img", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await sliderInfo.Image.CopyToAsync(stream);
            }
            await _context.SliderInfos.AddAsync(new SliderInfo
            {
                Description = sliderInfo.Description,
                Text = sliderInfo.Text,
                MiniText = sliderInfo.MiniText,
                Signature = fileName,
                IsMain=false
            });
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
            var sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);
            if (sliderInfo is null)
            {
                return NotFound();
            }
            string path = Path.Combine(_env.WebRootPath, "assets", "img", sliderInfo.Signature);
            path.Delete();
            _context.SliderInfos.Remove(sliderInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var exist = await _context.SliderInfos.FindAsync(id);
            if (exist is null) return NotFound();

            var vm = new SliderInfoEditVM
            {
                ImagePath = exist.Signature,
                SignaturePathName = exist.Signature,
                Description = exist.Description,
                Text = exist.Text,
                MiniText = exist.MiniText
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderInfoEditVM model)
        {
            if (id is null) return BadRequest();

            var exist = await _context.SliderInfos.FindAsync(id);
            if (exist is null) return NotFound();

            if (!ModelState.IsValid)
            {
                model.ImagePath = exist.Signature;
                return View(model);
            }

            if (model.Image != null)
            {
                if (!model.Image.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("Image", "Yalnız şəkil faylı seçin");
                    model.ImagePath = exist.Signature;
                    return View(model);
                }

                if (model.Image.Length / 1024 > 100)
                {
                    ModelState.AddModelError("Image", "Şəkil maksimum 100KB olmalıdır");
                    model.ImagePath = exist.Signature;
                    return View(model);
                }

                string fileName = Guid.NewGuid().ToString() + "-" + model.Image.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets", "img", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "assets", "img", exist.Signature);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                exist.Signature = fileName;
            }

            exist.Description = model.Description;
            exist.Text = model.Text;
            exist.MiniText = model.MiniText;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
