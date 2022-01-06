﻿using ClothingShop.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClothingShop.Entity.Models
{
    public class VoucherModel
    {
        public int VoucherId { get; set; }

        public int DiscountId { get; set; }

        public Discount Discount { get; set; }

        public string UserId { get; set; }

        public string Value { get; set; }

        [Display(Name = "Trạng thái")]
        public bool IsUsed { get; set; }
    }
}
