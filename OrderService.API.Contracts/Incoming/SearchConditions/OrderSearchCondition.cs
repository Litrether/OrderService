using OrderService.API.Contracts.Incoming.Abstractions;
using System;

namespace OrderService.API.Contracts
{
    public class OrderSearchCondition : PagedDTOBase
    {
        public string[] Status { get; set; }

        public int[] Cost { get; set; }

        public string[] Product { get; set; }

        public string[] Username { get; set; }

        public string[] DeliveryCompany { get; set; }

        public DateTime[] DeliveredAt { get; set; }

        public DateTime[] OrderedAt { get; set; }
    }
}
