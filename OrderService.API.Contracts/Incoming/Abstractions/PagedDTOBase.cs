using OrderService.API.Contracts.Extensions;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OrderService.API.Contracts.Incoming.Abstractions
{
    public class PagedDTOBase : PageSettings
    {
        public string SortDirection { get; set; } = "asc";

        public string SortProperty { get; set; }
    }
}
