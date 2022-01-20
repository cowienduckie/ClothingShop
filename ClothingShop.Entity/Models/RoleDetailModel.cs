using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Models
{
    public class RoleDetailModel
    {
        public string Id { get; set; }

        [Display(Name = "Vai trò")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Vai trò có độ dài 1 - 50 ký tự.")]
        [Required(ErrorMessage = "Vui lòng nhập Vai trò.")]
        public string RoleName { get; set; }
    }
}