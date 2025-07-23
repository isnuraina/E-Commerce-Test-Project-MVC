using E_Commerce_Test_Project_MVC.Data;
using E_Commerce_Test_Project_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Test_Project_MVC.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public SliderViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.AsNoTracking().ToListAsync();
            return await Task.FromResult(View(sliders));
        }
    }
}
