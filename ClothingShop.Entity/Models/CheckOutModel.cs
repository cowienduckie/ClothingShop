using ClothingShop.Entity.Entities;

namespace ClothingShop.Entity.Models
{
    public class CheckOutModel
    {
        public CheckOutModel()
        {
            Address = new Address();
        }

        public Cart Cart { get; set; }

        public Address Address { get; set; }

        public string DiscountCode { get; set; }

        public string Note { get; set; }
    }
}