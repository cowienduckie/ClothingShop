using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Category
    {
        [Required] [Key] public int CategoryId { get; set; }

        public int? ParentId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

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

        //
        public IList<ProductCategory> ProductCategories { get; set; }
    }
}