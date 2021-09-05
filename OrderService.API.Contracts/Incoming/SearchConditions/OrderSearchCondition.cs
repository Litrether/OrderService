using OrderService.API.Contracts.Incoming.Abstractions;

namespace OrderService.API.Contracts
{
    public class OrderSearchCondition : PagedDTOBase
    {
        public string[] Name { get; set; }
        public string[] Status { get; set; }

    }
}
