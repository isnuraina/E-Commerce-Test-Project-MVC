using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Test_Project_MVC.Models
{
    public class Category:BaseEntity
    {
        [Required(ErrorMessage ="This input can't be empty!")]
        [MaxLength(30,ErrorMessage ="Category length must be max 30!")]
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
