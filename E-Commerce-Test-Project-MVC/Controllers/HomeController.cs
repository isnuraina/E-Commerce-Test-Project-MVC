using E_Commerce_Test_Project_MVC.Data;
using E_Commerce_Test_Project_MVC.Models;
using E_Commerce_Test_Project_MVC.Services.Interfaces;
using E_Commerce_Test_Project_MVC.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace E_Commerce_Test_Project_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _accessor;
        public HomeController(AppDbContext context, ICategoryService categoryService, IProductService productService, IHttpContextAccessor accessor)
        {
            _context = context;
            _categoryService = categoryService;
            _productService = productService;
            _accessor = accessor;
        }
        public async Task<IActionResult> Index()
        {

            //_accessor.HttpContext.Response.Cookies.Append("name", "Nurana");
            //ViewBag.name = _accessor.HttpContext.Request.Cookies["name"];



            IEnumerable<Slider> sliders = await _context.Sliders.AsNoTracking().ToListAsync();
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            IEnumerable<Product> products = await _productService.GetAllAsync();
            SliderInfo sliderInfos = await _context.SliderInfos.AsNoTracking().FirstOrDefaultAsync(m=>m.IsMain==true);
            HomeVM model = new()
            {
                Sliders = sliders,
                Categories=categories,
                Products=products,
                SliderInfos=sliderInfos
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductToBasket(int id)
        {
            List<BasketVM> basketDatas = [];
            if (_accessor.HttpContext.Request.Cookies["basket"]!=null)
            {
                basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            var existBasketData = basketDatas.FirstOrDefault(m => m.ProductId == id);
            if (existBasketData is not null)
            {
                existBasketData.ProductCount++;
                existBasketData.Price = existBasketData.Price * existBasketData.ProductCount;
            }
            else
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound(); 
                }
                basketDatas.Add(new BasketVM { ProductId = id, ProductCount = 1, Price = product.Price });
            }
            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketDatas));
            return RedirectToAction(nameof(Index));
        }
    }
}
