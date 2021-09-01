using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;

namespace OrderService.API.Contracts.Incoming.Abstractions
{
    public class PagedDTOBase
    {
        [JsonIgnore]
        public ListSortDirection ListSortDirection => SortDirection.ToListSortDirection();

        public string SortDirection { get; set; } = "asc";

        public string SortProperty { get; set; }
    }
}
