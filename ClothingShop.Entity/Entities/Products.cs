using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Products : IdentityRole
    {
        public int ProductId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int Image { get; set; }

        //[Range(1, 1500), DataType(DataType.Currency)]
        public int Price { get; set; }

        [Range(1, 100)]
        public float Discount { get; set; }

        [StringLength(500)]
        public string Discription { get; set; }

        // [Display(Name = "Create Date"), DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        // [Display(Name = "LastModified Date"), DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }
    }
}