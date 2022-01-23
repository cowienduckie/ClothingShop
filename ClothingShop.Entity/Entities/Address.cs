using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Address
    {
        [Required]
        [Display(Name = "ID Địa chỉ")]
        public int AddressId { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Tỉnh/ Thành Phố")]
        public string Province { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Quận/ Huyện")]
        public string District { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Phường/ Xã")]
        public string Ward { get; set; }

        [StringLength(450)]
        [Required]
        [Display(Name = "Địa chỉ")]
        public string Detail { get; set; }

        [StringLength(15)]
        [Required]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Người nhận")]
        public string Receiver { get; set; }

        [Display(Name = "ID Tài khoản")] public string UserId { get; set; }

        public Users User { get; set; }

        public IList<Order> Orders { get; set; }
    }
}