using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ClothingShop.Entity.Entities
{
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int RankId { get; set; }

        public int CartId { get; set; }

        public int TotalPoint { get; set; }

        //
        public Rank Rank { get; set; }

        public IList<Order> Orders { get; set; }

        public Cart Cart { get; set; }

        public IList<Voucher> Vouchers { get; set; }

        public IList<Point> Points { get; set; }
    }
}