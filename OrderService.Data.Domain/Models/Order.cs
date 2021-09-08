using System;

namespace OrderService.Data.Domain.Models
{
    public class Order : KeyedEntityBase
    {
        public string Status { get; set; }

        public decimal Cost { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }

        public int DeliveryCompanyId { get; set; }
    }
}
