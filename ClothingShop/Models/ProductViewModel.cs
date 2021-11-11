using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ClothingShop.Models
{
    public class ProductViewModel
    {
        public List<Product> Name_Product { get; set; }
        //public string ProductType { get; set; }
        //public string SearchString { get; set; }
    }
}