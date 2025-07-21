using E_Commerce_Test_Project_MVC.Models;

namespace E_Commerce_Test_Project_MVC.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllTakenAsync(int take);
        Task<int> GetProductsCount();
    }
}
