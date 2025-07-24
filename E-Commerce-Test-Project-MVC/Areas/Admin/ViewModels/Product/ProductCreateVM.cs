using E_Commerce_Test_Project_MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Test_Project_MVC.Areas.Admin.ViewModels.Product
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Desc { get; set; }
        [Required]

        public int Price { get; set; }
        [Required]

        public List<IFormFile> Images { get; set; }
        public int CategoryId { get; set; }
    }
}
