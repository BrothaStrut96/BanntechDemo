using System;
using System.Collections.Generic;
using Chushka.Shared.Models.Base;

namespace Chushka.Shared.Models
{
    public class OrderDtoModel : BaseEntity
    {
        public int ProductId { get; set; }

        public ProductDtoModel Product  { get; set; }

        public int UserId { get; set; }

        public string Customer { get; set; }

        public DateTime OrderCreated { get; set; }
    }
}
