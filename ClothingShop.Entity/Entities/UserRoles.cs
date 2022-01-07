using Microsoft.AspNetCore.Identity;

namespace ClothingShop.Entity.Entities
{
    public class UserRoles : IdentityUserRole<string>
    {
        public Users User { get; set; }
        public Roles Role { get; set; }
    }
}