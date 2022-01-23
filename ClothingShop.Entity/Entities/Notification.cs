﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClothingShop.Entity.Entities
{
    public class Notification
    {
        [Required]
        [Key]
        public int NotificationId { get; set; }

        [Required]
        public string UserId { get; set; }

        public Users User { get; set; }

        public string Tittle { get; set; }

        [StringLength(1000), Required]
        public string Message { get; set; }

        [Required]
        public DateTime SendTime { get; set; }
    }
}
