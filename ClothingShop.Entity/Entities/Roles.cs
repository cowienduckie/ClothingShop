using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ClothingShop.Entity.Entities
{
    public class Roles : IdentityRole
    {
        public IList<UserRoles> UserRoles { get; set; }
    }
}