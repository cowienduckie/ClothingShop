using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClothingShop.Entity.Models
{
    public class CategoryModel
    {
        [Display (Name = "ID")]
        public int CategoryId { get; set; }

        [Display(Name = "Tên danh mục")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        //For creating 
        public bool IsSelected { get; set; }

        public CategoryModel()
        {
            IsSelected = false;
        }
    }
}
