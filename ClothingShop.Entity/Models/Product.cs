using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClothingShop.Entity.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name_Product { get; set; }


        [DataType(DataType.Date), Display(Name = "Date")]
        public DateTime DateCreated_Product { get; set; }

        [Display(Name = "Type of Product")]
        public string Type_Product { get; set; }
        [Column(TypeName = "decimal(18, 2)")]

        [Display(Name = "Price of Product")]
        public decimal Price_Product { get; set; }
    }
}
