using OrderService.API.Contracts.Incoming.Abstractions;
using System;

namespace OrderService.API.Contracts
{
    public class OrderSearchCondition : PagedDTOBase
    {
        public string[] Status { get; set; }

        public decimal[] Cost { get; set; }

        public string[] UserId { get; set; }

        public int[] ProductId { get; set; }

        public int[] DeliveryCompanyId { get; set; }
    }
}
