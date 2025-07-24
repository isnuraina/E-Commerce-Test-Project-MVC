using E_Commerce_Test_Project_MVC.Areas.Admin.ViewModels.Product;
using E_Commerce_Test_Project_MVC.Data;
using E_Commerce_Test_Project_MVC.Helpers.Extensions;
using E_Commerce_Test_Project_MVC.Models;
using E_Commerce_Test_Project_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce_Test_Project_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductService productService, AppDbContext context, IWebHostEnvironment env)
        {
            _productService = productService;
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var product = await _context.Products
                .Include(m=>m.Category)
                .Include(m=>m.ProductImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product is null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpGet]
        public async Task< IActionResult> Create()
        {
            ViewBag.categories = await GetCategoriesAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Create(ProductCreateVM request)
        {
           ViewBag.categories = await GetCategoriesAsync();
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            ICollection<ProductImage> images = new List<ProductImage>();


            foreach (var item in request.Images)
            {
                if (item.CheckFileSize(400))
                {
                    ModelState.AddModelError("Image", "Image size must be max 400KB");
                    return View(request);
                }

                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "Image type must be image format");
                    return View(request);
                }
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets", "img", fileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }
                images.Add(new ProductImage { Img = fileName });
            }
            images.FirstOrDefault().IsMain = true;
            Product product = new()
            {
                Name = request.Name,
                Description = request.Desc,
                Price = request.Price,
                CategoryId = request.CategoryId,
                ProductImages = images
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories.Select(a => new SelectListItem() { Value = a.Id.ToString(), Text = a.Name });

        }
    }
}
