using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingShop.Entity.Entities
{
    public class Rank
    {
        public int RankId { get; set; }

        public int NextRankId { get; set; }

        public string Name { get; set; }

        public int MinimumPoint { get; set; }

        [Range(0, 100)]
        [Required]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal ConvertPointPercentage { get; set; }

        //
        public IList<Users> Users { get; set; }
    }
}