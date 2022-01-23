using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClothingShop.Entity.Models
{
    public class UserDetailModel
    {
        public string Id { get; set; }

        [Display(Name = "Tên đăng nhập")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập có độ dài 3 - 50 ký tự.")]
        [Required(ErrorMessage = "Vui lòng nhập Tên đăng nhập")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Mật khẩu có độ dài 3 - 50 ký tự, bao gồm chữ cái và số.")]
        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Mật khẩu có độ dài 3 - 50 ký tự, bao gồm chữ cái và số.")]
        [Required(ErrorMessage = "Vui lòng nhập lại Mật khẩu")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [StringLength(200, MinimumLength = 3, ErrorMessage = "Email có độ dài 3 - 200 ký tự.")]
        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        public string Email { get; set; }

        public List<SelectListItem> Roles { get; set; }

        [Display(Name = "Role")] public string RoleId { get; set; }
    }
}