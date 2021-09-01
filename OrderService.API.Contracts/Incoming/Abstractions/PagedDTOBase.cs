using OrderService.API.Contracts.Extensions;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace OrderService.API.Contracts.Incoming.Abstractions
{
    public class PagedDTOBase : PageSettings
    {
        [JsonIgnore]
        public ListSortDirection ListSortDirection => SortDirection.ToListSortDirection();

        public string SortDirection { get; set; } = "asc";

        public string SortProperty { get; set; }
    }
}
