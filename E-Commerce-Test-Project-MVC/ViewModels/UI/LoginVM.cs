using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Test_Project_MVC.ViewModels.UI
{
    public class LoginVM
    {
        [Required]
        public string EmailOrUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
