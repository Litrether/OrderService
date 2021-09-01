using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.API.Contracts.Outgoing.Abstractions
{
    public class Response
    {
        public bool Success { get; set; }

        public static Response Succefull { get; } = new Response { Success = true };

        public static Response Error { get; } = new Response { Success = false };
    }
}
