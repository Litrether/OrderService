namespace OrderService.Data.Domain.Models
{
    public class DeliveryCompany: KeyedEntityBase
    {
        public string Name { get; set; }

        public double Rating { get; set; }
    }
}
