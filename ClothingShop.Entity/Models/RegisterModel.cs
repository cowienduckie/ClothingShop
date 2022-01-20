using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Models
{
    public class RegisterModel
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

        [Display(Name = "Tên")]
        [StringLength(50, ErrorMessage = "Tên có độ dài tối đa 50 ký tự.")]
        [Required(ErrorMessage = "Vui lòng nhập Tên")]
        public string FirstName { get; set; }

        [Display(Name = "Họ và tên đệm")]
        [StringLength(50, ErrorMessage = "Họ và tên đệm có độ dài tối đa 50 ký tự.")]
        [Required(ErrorMessage = "Vui lòng nhập Họ và tên đệm")]
        public string LastName { get; set; }
    }
}