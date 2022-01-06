using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ClothingShop.Entity.Entities
{
    public class UserRoles : IdentityUserRole<string>
    {
        public Users User { get; set; }
        public Roles Role { get; set; }
    }
}
