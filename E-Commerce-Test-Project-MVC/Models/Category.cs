﻿namespace E_Commerce_Test_Project_MVC.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
