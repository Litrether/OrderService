using System;

namespace OrderService.API.Contracts.Outgoing
{
    public class FoundOrderDTO
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public double Cost { get; set; }

        public string Username { get; set; }

        public int ProductId { get; set; }

        public int DeliveryCompanyId { get; set; }
    }
}
