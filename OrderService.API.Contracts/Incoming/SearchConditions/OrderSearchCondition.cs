using OrderService.API.Contracts.Incoming.Abstractions;

namespace OrderService.API.Contracts
{
    public class OrderSearchCondition : PagedDTOBase
    {
        public string[] Status { get; set; }

        public double[] Cost { get; set; }

        public string[] Username { get; set; }

        public int[] ProductId { get; set; }

        public int[] DeliveryCompanyId { get; set; }
    }
}
