using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClothingShop.Entity.Validation;
using Microsoft.AspNetCore.Http;

namespace ClothingShop.Entity.Models
{
    public class ProductDetailModel
    {
        public ProductDetailModel()
        {
            Image = "https://i.imgur.com/iQeIsmz.jpg";
            Items = new List<ItemModel>();
            Colors = new List<ColorModel>();
            Sizes = new List<SizeModel>();
            Categories = new List<CategoryModel>();
            Description =
                "Đây là một mô tả mẫu để thử nghiệm cho sản phẩm của cửa hàng thời trang Eva de Eva. Các thành viên phụ trách sản phẩm phải update lại mô tả này. Độ dài của mô tả này là 180 ký tự.";
        }

        [Display(Name = "ID")] public int ProductId { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Ảnh đại diện")]
        public string Image { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,#VND}")]
        [Display(Name = "Giá")]
        [Required]
        public int Price { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Số lượng")] public int Stock { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày thêm")]
        public DateTime CreateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Chỉnh sửa cuối")]
        public DateTime LastModified { get; set; }

        public List<ItemModel> Items { get; set; }

        public List<ColorModel> Colors { get; set; }

        public List<SizeModel> Sizes { get; set; }

        [Display(Name = "Danh mục sản phẩm")] public List<CategoryModel> Categories { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(1 * 1024 * 1024)] //size = x * 1024 * 1024 MB
        [AllowedExtensions(new[] {".jpg", ".png", ".jpeg"})]
        public IFormFile UploadImage { get; set; }
    }
}