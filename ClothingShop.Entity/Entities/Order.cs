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
        [Key]
        public int OrderId { get; set; }

        public string UserId { get; set; }

<<<<<<< Updated upstream
=======
        public int AddressId { get; set; }

        [Display(Name = "Gi� g?c")]
        [DisplayFormat(DataFormatString = "{0:#,#VND;;0VND}")]
>>>>>>> Stashed changes
        public int OriginalPrice { get; set; }


        [Display(Name = "Chi?t kh?u")]
        [DisplayFormat(DataFormatString = "{0:#,#VND;;0VND}")]
        public int Discount { get; set; }


        [Display(Name = "T?ng gi�")]
        [DisplayFormat(DataFormatString = "{0:#,#VND;;0VND}")]
        public int TotalPrice { get; set; }

        [Display(Name = "Tr?ng th�i")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Create Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        //
        public Point Point { get; set; }

        public Users User { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }
}