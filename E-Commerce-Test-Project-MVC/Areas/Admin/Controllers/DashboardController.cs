using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Test_Project_MVC.Areas.Admin.Controllers
{
    [Area("admin")]
   
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
