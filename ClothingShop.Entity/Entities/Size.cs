using Microsoft.AspNetCore.Identity;

namespace ClothingShop.Entity.Entities
{
    public class Size : IdentityRole
    {
        public int SizeId { get; set; }

        public int Value { get; set; }
    }
}