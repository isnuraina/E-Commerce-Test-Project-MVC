using E_Commerce_Test_Project_MVC.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Commerce_Test_Project_MVC.Controllers
{
    public class BasketController : Controller
    {
        private readonly IHttpContextAccessor _accessor;

        public BasketController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

       
        public IActionResult Index()
        {
            var basketJson = _accessor.HttpContext.Request.Cookies["basket"];
            List<BasketVM> basket = new();

            if (!string.IsNullOrEmpty(basketJson))
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(basketJson);
            }

            return View(basket);
        }
    }
}
