using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ClothingShop.Entity.Entities
{
    public class Roles : IdentityRole
    {
        public IList<UserRoles> UserRoles { get; set; }
    }
}