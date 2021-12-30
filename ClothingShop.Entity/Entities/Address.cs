using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingShop.Entity.Entities
{
    public class Address
    {
        public int AddressId { get; set; }

        public string Province { get; set; }

        public string District { get; set; }

        public string Ward { get; set; }

        public string Detail { get; set; }

        public string UserId { get; set; }

        public Users User { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
