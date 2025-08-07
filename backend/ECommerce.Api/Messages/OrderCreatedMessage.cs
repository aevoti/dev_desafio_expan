using System;
using System.Collections.Generic;

namespace OrderApi.Messages
{
    public class OrderCreatedMessage
    {
        public Guid OrderId { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
