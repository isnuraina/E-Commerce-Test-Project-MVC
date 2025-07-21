using E_Commerce_Test_Project_MVC.Models;

namespace E_Commerce_Test_Project_MVC.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>>GetAllAsync();
    }
}
