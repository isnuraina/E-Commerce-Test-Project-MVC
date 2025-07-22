using E_Commerce_Test_Project_MVC.Data;
using E_Commerce_Test_Project_MVC.Models;
using E_Commerce_Test_Project_MVC.Services.Interfaces;
using E_Commerce_Test_Project_MVC.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce_Test_Project_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public HomeController(AppDbContext context, ICategoryService categoryService, IProductService productService)
        {
            _context = context;
            _categoryService = categoryService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
           
            IEnumerable<Slider> sliders = await _context.Sliders.AsNoTracking().ToListAsync();
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            IEnumerable<Product> products = await _productService.GetAllAsync();
            SliderInfo sliderInfos = await _context.SliderInfos.AsNoTracking().FirstOrDefaultAsync();
            HomeVM model = new()
            {
                Sliders = sliders,
                Categories=categories,
                Products=products,
                SliderInfos=sliderInfos
            };
            return View(model);
        }
    }
}
