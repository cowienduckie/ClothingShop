using Microsoft.AspNetCore.Identity;

namespace ClothingShop.Entity.Entities
{
    public class Product_Category : IdentityRole
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }
    }
}