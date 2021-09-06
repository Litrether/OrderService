using Microsoft.EntityFrameworkCore;
using OrderService.API.Contracts;
using OrderService.Data.Domain.Models;
using OrderService.Data.EF.SQL;
using OrderService.Data.Services.Abstraction;
using OrderService.Data.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Data.Services
{
    public interface IOrderViewService : IBaseService<OrderView>
    {
        Task<IReadOnlyCollection<OrderView>> FindAsync(OrderSearchCondition searchCondition, string sortProperty);
        Task<int> CountAsync(OrderSearchCondition searchCondition);
        Task<bool> ExistsAsync(int id, OrderSearchCondition cancellationToken);
    }

    public class OrderViewService : BaseService<OrderView>, IOrderViewService
    {
        private readonly OrderServiceDbContext dbContext;

        public OrderViewService(OrderServiceDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<OrderView>> FindAsync(OrderSearchCondition searchCondition, string sortProperty)
        {
            IQueryable<OrderView> query = BuildFindQuery(searchCondition);

            query = searchCondition.SortDirection == "asc"
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.PageSize, searchCondition.Page).ToListAsync();
        }

        public async Task<int> CountAsync(OrderSearchCondition searchCondition)
        {
            IQueryable<OrderView> query = BuildFindQuery(searchCondition);

            var count = await query.CountAsync();

            return count % 10 == 0 ? count / 10 : count / 10 + 1;
        }

        public Task<bool> ExistsAsync(int id, OrderSearchCondition cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        private IQueryable<OrderView> BuildFindQuery(OrderSearchCondition searchCondition)
        {
            IQueryable<OrderView> query = dbContext.OrdersViews;

            //todo searchCondition
            return null;
        }
    }
}
