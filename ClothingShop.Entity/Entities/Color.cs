using Microsoft.AspNetCore.Identity;

namespace ClothingShop.Entity.Entities
{
    public class Color : IdentityRole
    {
        public int ColorId { get; set; }

        //Each color has a unique code 
        public int Value { get; set; }
    }
}