using E_Commerce_Test_Project_MVC.Models;
using E_Commerce_Test_Project_MVC.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace E_Commerce_Test_Project_MVC.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _accessor;

        public HeaderViewComponent(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BasketVM> basketDatas = [];
            Dictionary<string, string> settings = new();
            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                  basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            int count = basketDatas.Sum(m => m.ProductCount);
            decimal sum = basketDatas.Sum(m => m.Price);


            return await Task.FromResult(View(new HeaderVM {Settings=settings,ProductCountOfBasket=count, ProductSumOfBasket=sum }));
        }
    }
}
