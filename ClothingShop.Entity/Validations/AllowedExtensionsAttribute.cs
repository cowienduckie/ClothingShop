using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ClothingShop.Entity.Validation
{
    [AttributeUsage(AttributeTargets.All)]
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null && file.Length != 0)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower())) return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            var message = "Ảnh đại diện chỉ chấp nhận đuôi";
            for (var i = 0; i < _extensions.Length; ++i)
                if (i != _extensions.Length - 1)
                    message += $" {_extensions[i]},";
                else
                    message += $" {_extensions[i]}";
            return message;
        }
    }
}