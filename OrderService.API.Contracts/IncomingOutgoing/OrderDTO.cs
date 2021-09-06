using System;

namespace OrderService.API.Contracts.IncomingOutgoing
{
    public class OrderDTO
    {
        public string Status { get; set; }

        public decimal Cost { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }

        public int DeliveryCompanyId { get; set; }

        public DateTime OrderedAt { get; set; }

        public DateTime DeliveredAt { get; set; }
    }
}
