using E_Commerce_Test_Project_MVC.Data;
using E_Commerce_Test_Project_MVC.Models;
using E_Commerce_Test_Project_MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Test_Project_MVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(m=>m.Products)
                .AsNoTracking().ToListAsync();
        }
    }
}
