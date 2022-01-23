using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Color
    {
        [Required] public int ColorId { get; set; }

        [Required] public string Value { get; set; }

        public string ColorHexCode { get; set; }

        public IList<ProductEntry> ProductEntries { get; set; }
    }
}