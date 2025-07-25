using Microsoft.AspNetCore.Identity;

namespace E_Commerce_Test_Project_MVC.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
