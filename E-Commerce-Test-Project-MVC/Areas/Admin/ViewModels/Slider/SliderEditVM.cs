using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Test_Project_MVC.Areas.Admin.ViewModels.Slider
{
    public class SliderEditVM
    {
        public string? ImagePath { get; set; }
        public IFormFile? Image { get; set; } 
    }

}

