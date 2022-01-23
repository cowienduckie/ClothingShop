using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ đăng nhập?")] public bool RememberMe { get; set; }
    }
}