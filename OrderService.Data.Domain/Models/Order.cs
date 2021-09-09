using System;

namespace OrderService.Data.Domain.Models
{
    public class Order : KeyedEntityBase
    {
        public string Status { get; set; }

        public double Cost { get; set; }

        public string Username { get; set; }

        public int ProductId { get; set; }

        public int DeliveryCompanyId { get; set; }
    }
}
