namespace OrderService.API.Contracts.Incoming.Abstractions
{
    public class PagedDTOBase : PageSettings
    {
        public string SortDirection { get; set; } = "asc";

        public string SortProperty { get; set; }
    }
}
