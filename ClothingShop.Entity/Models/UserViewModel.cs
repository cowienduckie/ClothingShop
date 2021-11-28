using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingShop.Entity.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> RoleNames { get; set; }
    }
}
