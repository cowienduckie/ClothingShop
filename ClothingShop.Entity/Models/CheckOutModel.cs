using ClothingShop.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingShop.Entity.Models
{
    public class CheckOutModel
    {
        public Cart Cart { get; set; }

        public Address Address { get; set; }

        public string DiscountCode { get; set; }

        public CheckOutModel()
        {
            Address = new Address();
        }
    }
}
