using System;
using System.Collections.Generic;
using Chushka.Shared.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace Chushka.Data.Models
{
    public class OrderModel : BaseEntity
    {
        public int ProductId { get; set; }

        public virtual ProductModel Product { get; set; }

        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime OrderCreated { get; set; }
    }
}
