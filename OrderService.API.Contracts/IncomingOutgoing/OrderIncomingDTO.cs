namespace OrderService.API.Contracts.IncomingOutgoing
{
    public class OrderIncomingDTO
    {
        public string Status { get; set; }

        public double Cost { get; set; }

        public string Username { get; set; }

        public int ProductId { get; set; }

        public int DeliveryCompanyId { get; set; }
    }
}
