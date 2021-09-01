using System;

namespace OrderService.Data.Domain.Models
{
    public enum OrderStatus
    {
        Processed,
        On_the_way,
        Delivered,
    }

    public class Order : KeyedEntityBase
    {
        public OrderStatus Status { get; set; }

        public decimal Cost { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }

        public DateTime OrderedAt { get; set; }

        public DateTime DeliveredAt { get; set; }

        public int DeliveryCompanyId { get; set; }
        public DeliveryCompany DeliveryCompany { get; set; }
    }
}
