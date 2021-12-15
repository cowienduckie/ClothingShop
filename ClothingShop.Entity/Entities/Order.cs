using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClothingShop.Entity.Entities;

namespace ClothingShop.Entity.Entities
{
    public class Order
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        public int OriginalPrice { get; set; }

        public int Discount { get; set; }

        public int TotalPrice { get; set; }

        public string Status { get; set; }

        [Required]
        [Display(Name = "Create Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }
    }
}