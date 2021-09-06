using OrderService.API.Contracts.Incoming.Abstractions;

namespace OrderService.API.Contracts.Incoming.SearchConditions
{
    public class DeliveryCompanySearchCondition : PagedDTOBase
    {
        public string[] Name { get; set; }

        public double[] Rating { get; set; }
    }
}
