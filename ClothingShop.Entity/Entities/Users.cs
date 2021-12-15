using Microsoft.AspNetCore.Identity;

namespace ClothingShop.Entity.Entities
{
    public class Users : IdentityUser
    {
        public int RankId { get; set; }

        public int TotalPoint { get; set; }
    }
}