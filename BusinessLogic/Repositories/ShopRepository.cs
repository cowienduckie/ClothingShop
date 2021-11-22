using ClothingShop.BusinessLogic.Repositories.Interfaces;
using ClothingShop.Entity.Data;
using ClothingShop.Entity.Entities;

namespace ClothingShop.BusinessLogic.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly ShopContext _db;
        

        public ShopRepository(ShopContext db)
        {
            _db = db;
        }
        public string getUser(string email)
        {
            
            return "";
        }
    }
}