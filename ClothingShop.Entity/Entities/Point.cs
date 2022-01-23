using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Point
    {
        [Required] [Key] public int PointId { get; set; }

        public string UserId { get; set; }

        public int OrderId { get; set; }

        [Required] public int Value { get; set; }

        [Required]
        [Display(Name = "Create Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        [Required]
        [Display(Name = "Last Modified")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastModified { get; set; }

        public bool IsValid { get; set; }

        //
        public Users User { get; set; }

        public Order Order { get; set; }
    }
}