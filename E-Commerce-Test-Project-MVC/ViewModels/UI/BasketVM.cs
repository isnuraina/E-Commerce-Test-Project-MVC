namespace E_Commerce_Test_Project_MVC.ViewModels.UI
{
    public class BasketVM
    {
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }     
        public string ImageUrl { get; set; }
    }
}
