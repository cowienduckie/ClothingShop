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
        [Required, Key]
        public int VoucherId { get; set; }

        [Required, Key]
        public int DiscountId { get; set; }

        [Required, Key]
        public int UserId { get; set; }

        public string Value { get; set; }
    }
}