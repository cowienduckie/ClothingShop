using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace ClothingShop.Entity.Entities
{
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [AllowNull] public DateTime? DateOfBirth { get; set; }

        [AllowNull] public int? RankId { get; set; }

        public int? CartId { get; set; }

        [AllowNull] public int? TotalPoint { get; set; }

        //
        public Rank Rank { get; set; }

        public IList<Order> Orders { get; set; }

        public Cart Cart { get; set; }

        public IList<Voucher> Vouchers { get; set; }

        public IList<Point> Points { get; set; }

        public IList<Notification> Notifications { get; set; }

        public IList<Address> Addresses { get; set; }

        public IList<UserRoles> UserRoles { get; set; }
    }
}