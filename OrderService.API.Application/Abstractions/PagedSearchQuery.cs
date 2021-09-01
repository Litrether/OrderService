using MediatR;
using OrderService.API.Contracts.Incoming.Abstractions;
using OrderService.API.Contracts.Outgoing.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderService.API.Application.Abstractions
{
    //todo PagedSearchQuery
    public class PagedSearchQuery<TFound> 
    {
        public PagedSearchQuery()
        { }
    }

    public class PagedSearchQuery<TFound, TSearchCondition> : IRequest<PagedResponse<TFound>>
        where TSearchCondition : PagedDTOBase
    {
        public TSearchCondition SearchCondition { get; }

        public PagedSearchQuery(TSearchCondition searchCondition)
        {
            SearchCondition = searchCondition;
        }
    }
}
