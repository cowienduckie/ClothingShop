using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClothingShop.Entity.Entities;

namespace ClothingShop.Entity.Models
{
    public class ProductDetailModel
    {
        [Display(Name = "ID")]
        public int ProductId { get; set; }

        [StringLength(50), Required]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Ảnh đại diện")]
        public string Image { get; set; }

        [Required, DataType(DataType.Currency)]
        [Display(Name = "Giá")]
        public int Price { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = true)]
        [Display(Name="Chiết khấu")]
        [Range(0, 100, ErrorMessage = "Chiết khấu chỉ nằm trong khoảng 0 - 100%")]
        public decimal? Discount { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Tồn kho")]
        public int Stock { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày thêm")]
        public DateTime CreateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Chỉnh sửa cuối")]
        public DateTime LastModified { get; set; }

        public List<ItemModel> Items { get; set; }

        public ProductDetailModel()
        {
            this.Discount = 0;
            this.Image = "https://imgur.com/a/tZDUgE8";
            this.Items = new List<ItemModel>();
            this.Description = "Đây là một mô tả mẫu để thử nghiệm cho sản phẩm của cửa hàng thời trang Eva de Eva. Các thành viên phụ trách sản phẩm phải update lại mô tả này. Độ dài của mô tả này là 180 ký tự.";
        }
    }
}