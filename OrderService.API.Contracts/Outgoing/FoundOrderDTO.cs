using System;

namespace OrderService.API.Contracts.Outgoing
{
    public class FoundOrderDTO
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public decimal Cost { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }

        public int DeliveryCompanyId { get; set; }
    }
}
