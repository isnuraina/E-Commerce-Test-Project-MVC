using E_Commerce_Test_Project_MVC.Data;
using E_Commerce_Test_Project_MVC.Models;
using E_Commerce_Test_Project_MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Test_Project_MVC.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(m=>m.Category)
                .Include(m => m.ProductImages)
                .ToListAsync();
        }

      
        public async Task<IEnumerable<Product>>GetAllTakenAsync(int take)
        {
            return await _context.Products.Include(x => x.ProductImages).Take(take).ToListAsync();
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(m => m.ProductImages)
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            return product;
        }

        public async Task<int> GetProductsCount()
        {
            return await _context.Products.CountAsync();
        }
    }
}
