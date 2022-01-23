using System.Security.Cryptography;
using System.Text;

namespace ClothingShop.BusinessLogic.Helpers
{
    public static class ShopHelper
    {
        public static string MD5Hash(string input)
        {
            var hash = new StringBuilder();
            var md5provider = new MD5CryptoServiceProvider();
            var bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (var i = 0; i < bytes.Length; i++) hash.Append(bytes[i].ToString("x2"));
            return hash.ToString();
        }
    }
}