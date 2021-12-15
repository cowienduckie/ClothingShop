using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClothingShop.Entity.Entities
{
    public class Rank
    {
        [Required, Key]
        public int RankId { get; set; }
        
        public int NextRankId { get; set; }

        public string Name { get; set; }

        public int MinimumPoint { get; set; }

        public decimal ConvertPointPercentager { get; set; }
    }
}
