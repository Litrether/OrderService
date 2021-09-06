using System;

namespace OrderService.Data.Domain.Models
{
    public class OrderView : KeyedEntityBase
    {
        public string Status { get; set; }

        public decimal Cost { get; set; }

        public string Username { get; set; }

        public string Product { get; set; }

        public DateTime OrderedAt { get; set; }

        public DateTime DeliveredAt { get; set; }

        public string DeliveryCompany { get; set; }
    }
}
