using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ClothingShop.BusinessLogic.Helpers
{
    public static class ImageHelper
    {
        public static string UploadImage(IFormFile FileUpload, int id = -1)
        {
            var ext = Path.GetExtension(FileUpload.FileName);
            var fileName = id != -1
                ? $"{id}_{DateTime.Now:yyyyMMddHHmmss}{ext}"
                : $"img_{DateTime.Now:yyyyMMddHHmmss}{ext}";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/img",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                FileUpload.CopyTo(stream);
            }

            var filePath = $"~/img/{fileName}";

            return filePath;
        }
    }
}