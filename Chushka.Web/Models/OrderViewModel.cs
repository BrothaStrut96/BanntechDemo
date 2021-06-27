using System;
using System.Collections.Generic;
using Chushka.Data.Models;
using Chushka.Shared.Models;

namespace Chushka.Web.Models
{
    public class OrderViewModel
    {
        public List<OrderDtoModel> Orders { get; set; }
    }

    public class CreateOrderViewModel
    {
        public int ProductId { get; set; }

        public int UserId { get; set; }

        public OrderModel DtoToEntitiy(OrderDtoModel model)
        {
            return new OrderModel()
            {
                ProductId = model.ProductId,
                UserId = model.UserId,
                OrderCreated = model.OrderCreated
            };
        }
    }
}
