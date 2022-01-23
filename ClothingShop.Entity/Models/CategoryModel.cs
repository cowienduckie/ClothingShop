using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Models
{
    public class CategoryModel
    {
        public CategoryModel()
        {
            IsSelected = false;
        }

        [Display(Name = "ID")] public int CategoryId { get; set; }

        [Display(Name = "Tên danh mục")] public string Name { get; set; }

        [Display(Name = "Mô tả")] public string Description { get; set; }

        //For creating
        public bool IsSelected { get; set; }
    }
}