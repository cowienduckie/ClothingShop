using System.Collections.Generic;

namespace ClothingShop.Entity.Models
{
    public class PaginationModel<T>
    {
        public List<T> ItemList { get; set; }

        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}