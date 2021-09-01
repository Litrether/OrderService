using System;

namespace OrderService.API.Contracts.Outgoing.Abstractions
{
    public class PagedResponse<TFoundItem>
    {
        public TFoundItem[] Items { get; set; }
        public int TotalCount { get; set; }

        public PagedResponse()
        {
            Items = Array.Empty<TFoundItem>();
        }
    }
}
