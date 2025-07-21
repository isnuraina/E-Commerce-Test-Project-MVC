using E_Commerce_Test_Project_MVC.Models;

namespace E_Commerce_Test_Project_MVC.ViewModels.UI
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
