using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClothingShop.Entity.Models
{
    public class RankModel
    {
        public int RankId { get; set; }

        public int NextRankId { get; set; }

        public RankModel NextRank { get; set; }

        public string Name { get; set; }

        public int MinimumPoint { get; set; }

        [Range(0, 1), Required]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal ConvertPointPercentage { get; set; }
    }
}
