using E_Commerce_Test_Project_MVC.Data;
using E_Commerce_Test_Project_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Test_Project_MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task <IActionResult> Index()
        {
            ViewBag.count = await _productService.GetProductsCount();
           
            return View(await _productService.GetAllTakenAsync(4));
        }
    }
}
