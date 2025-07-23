namespace E_Commerce_Test_Project_MVC.Areas.Admin.ViewModels.SliderInfo
{
    public class SliderInfoEditVM
    {
        public string? ImagePath { get; set; } 
        public IFormFile? Image { get; set; }

        public string SignaturePathName { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string MiniText { get; set; }
    }
}
