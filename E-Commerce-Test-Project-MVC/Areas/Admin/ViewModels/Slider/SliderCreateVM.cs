using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Test_Project_MVC.Areas.Admin.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required]
           public IFormFile Image { get; set; }
    }
}
