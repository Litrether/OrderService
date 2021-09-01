using OrderService.API.Contracts;
using OrderService.Data.Domain.Models;
using OrderService.Data.EF.SQL;
using OrderService.Data.Services.Abstraction;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data.Services
{
    public interface IOrderService : IBaseService<Order>
    {
        Task<IReadOnlyCollection<Order>> FindAsync(OrderSearchCondition searchCondition, string sortProperty);
        Task<int> CountAsync(OrderSearchCondition searchCondition);
        Task<bool> ExistsAsync(int id);
    }

    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly OrderServiceDbContext dbContext;

        public OrderService(OrderServiceDbContext dbContext) : base(dbContext) =>
            this.dbContext = dbContext;

        public Task<bool> ExistsAsync(int id) =>
            dbContext.Order.AnyAsync(entity => entity.id);

        public async Task<IReadOnlyCollection<Order>> FindAsync(OrderSearchCondition searchCondition, string sortProperty)
        {
            IQueryable<Order> query = BuildFindQuery(searchCondition);


            query = searchCondition.ListSortDirection == ListSortDirection.Ascending
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.)
        }

        public Task<int> CountAsync(OrderSearchCondition searchCondition)
        {
        }

        private IQueryable<Order> BuildFindQuery(OrderSearchCondition searchCondition)
        {
            IQueryable<Order> query = dbContext.Orders;

            if (searchCondition.Name.Any())
                foreach (var name in searchCondition.Name)
                {
                    var upperName = name.ToUpper().Trim();
                    query = query.Where(x =>
                    x.Name != null && x.Name.ToUpper().Contains(upperName));
                }

            return query;
        }
    }
}
