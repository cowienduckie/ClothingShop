<<<<<<<< HEAD:ClothingShop.Entity/Models/ErrorViewModel.cs
namespace ClothingShop.Entity.Models
========
using System;

namespace ClothingShop.Models
>>>>>>>> master:ClothingShop/Models/ErrorViewModel.cs
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}