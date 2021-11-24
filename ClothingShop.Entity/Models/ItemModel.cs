using System;
using System.Collections.Generic;
using System.Text;

namespace ClothingShop.Entity.Models
{
    public class ItemModel
    {
        public int ColorId { get; set; }

        public int SizeId { get; set; }

        public string ColorValue { get; set; }
        
        public string ColorHexCode { get; set; }

        public string SizeValue { get; set; }

        public string SKU { get; set; }

        public int Quantity { get; set; }
    }
}
