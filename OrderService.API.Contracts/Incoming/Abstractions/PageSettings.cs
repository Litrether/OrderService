using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.API.Contracts.Incoming.Abstractions
{
    public class PageSettings
    {
        public int PageSize { get; set; }

        public int Page { get; set; }
    }
}
