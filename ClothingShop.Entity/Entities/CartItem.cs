using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClothingShop.Entity.Entities;

namespace ClothingShop.Entity.Entities
{
    public class CartItem
    {
        [Required]
        public int CartItemId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int DiscountId { get; set; }
    }
}
