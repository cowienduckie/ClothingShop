using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Product_Entry : IdentityRole
    {
        public int ProductId { get; set; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }

        [StringLength(30)]
        public string SKU { get; set; }

        public int Quantity { get; set; }
    }
}