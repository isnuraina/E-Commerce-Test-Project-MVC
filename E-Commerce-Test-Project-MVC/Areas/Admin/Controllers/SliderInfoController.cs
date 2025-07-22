using E_Commerce_Test_Project_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Test_Project_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderInfoController : Controller
    {
        private readonly AppDbContext _context;

        public SliderInfoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var sliderInfo = await _context.SliderInfos.AsNoTracking().ToListAsync();
            return View(sliderInfo);
        }
    }
}
