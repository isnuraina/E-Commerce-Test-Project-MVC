using E_Commerce_Test_Project_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Test_Project_MVC.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Slider>  Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
    }
}
