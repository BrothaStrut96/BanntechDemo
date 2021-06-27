using System;
using Chushka.Shared.Models.Base;
using Chuska.Shared.Models.Enums;

namespace Chushka.Shared.Models
{
    public class ProductDtoModel : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public ProductType Type { get; set; }
    }
}
