using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClothingShop.Entity.Entities;

namespace ClothingShop.Entity.Entities
{
    public class Point
    {
        [Required, Key]
        public int PointId { get; set; }

        [Required, Key]
        public int UserId { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        [Display(Name = "Create Time"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        [Required]
        [Display(Name = "Last Modified"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }

        public bool IsValid { get; set; }
    }
}
