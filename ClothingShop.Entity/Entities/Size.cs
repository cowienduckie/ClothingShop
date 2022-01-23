using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingShop.Entity.Entities
{
    public class Size
    {
        [Required] public int SizeId { get; set; }

        [Required] public string Value { get; set; }

        public IList<ProductEntry> ProductEntries { get; set; }
    }
}