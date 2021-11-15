using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingShop.Entity.Entities
{
    public class Product
    {
        [Required]
        public int ProductId { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [Required, DataType(DataType.Currency)]
        public long Price { get; set; }

        [Range(1, 100)]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal? Discount { get; set; }

        [StringLength(500)]
        public string Discription { get; set; }

        [Required]
        [Display(Name = "Create Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        [Required]
        [Display(Name = "LastModified Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }
    }
}