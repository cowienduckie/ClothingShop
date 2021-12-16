using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClothingShop.Entity.Entities;

namespace ClothingShop.Entity.Entities
{
    public class Cart
    {
        [Required, Key]
        public int CartId { get; set; }

        [Required, Key]
        public int UserId { get; set; }

        public int OriginalPrice { get; set; }

        public int Discount { get; set; }

        public int TotalPrice { get; set; }

        [Required]
        [Display(Name = "Create Time"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        [Required]
        [Display(Name = "Last Modified"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }

        [Required]
        public bool IsDelete { get; set; }

        //
        public Users User { get; set; }

        public IList<CartItem> CartItems { get; set; }
    }
}