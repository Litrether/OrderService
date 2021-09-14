using MediatR;
using OrderService.API.Contracts.Incoming.Abstractions;
using OrderService.API.Contracts.Outgoing.Abstractions;

namespace OrderService.API.Application.Abstractions
{
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
