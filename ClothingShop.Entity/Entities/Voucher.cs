using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClothingShop.Entity.Entities;

namespace ClothingShop.Entity.Entities
{
    public class Voucher
    {
        public int VoucherId { get; set; }

        public int DiscountId { get; set; }

        public string UserId { get; set; }

        public string Value { get; set; }

        public bool IsUsed { get; set; }

        //
        public Discount Discount { get; set; }
        public Users User { get; set; }
    }
}