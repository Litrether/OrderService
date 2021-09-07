using Microsoft.EntityFrameworkCore;
using OrderService.API.Contracts;
using OrderService.Data.Domain.Models;
using OrderService.Data.EF.SQL;
using OrderService.Data.Services.Abstraction;
using OrderService.Data.Services.Extensions;
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
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    }

    public class OrderViewService : BaseService<OrderView>, IOrderViewService
    {
        private readonly DatabaseContext dbContext;

        public OrderViewService(DatabaseContext dbContext) : base(dbContext)
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

        public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken) =>
            dbContext.Orders.AnyAsync(entity => entity.Id == id, cancellationToken);

        private IQueryable<OrderView> BuildFindQuery(OrderSearchCondition searchCondition)
        {
            IQueryable<OrderView> query = dbContext.OrdersViews;

            if (searchCondition.Status.Any())
                foreach (var status in searchCondition.Status)
                {
                    var upperStatus = status.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Status != null && x.Status.ToUpper().Contains(upperStatus));
                }

            if (searchCondition.Cost.Any())
                foreach (var cost in searchCondition.Cost)
                    query = query.Where(x => x.Cost == cost);

            if (searchCondition.Username.Any())
                foreach (var username in searchCondition.Username)
                {
                    var upperUsername = username.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Username != null && x.Username.ToUpper().Contains(upperUsername));
                }

            if (searchCondition.DeliveryCompany.Any())
                foreach (var deliveryCompany in searchCondition.DeliveryCompany)
                {
                    var upperDeliveryCompany = deliveryCompany.ToUpper().Trim();
                    query = query.Where(x =>
                        x.DeliveryCompany != null &&
                        x.DeliveryCompany.ToUpper().Contains(upperDeliveryCompany));
                }

            if (searchCondition.OrderedAt.Any())
                foreach (var orderedAt in searchCondition.OrderedAt)
                    query = query.Where(x => x.OrderedAt == orderedAt);

            if (searchCondition.DeliveredAt != null)
                foreach (var deliveredAt in searchCondition.DeliveredAt)
                    query = query.Where(x => x.DeliveredAt == deliveredAt);

            if (searchCondition.Product.Any())
                foreach (var product in searchCondition.Product)
                {
                    var upperProduct = product.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Product != null && x.Product.ToUpper().Contains(upperProduct));
                }

            return query;
        }
    }
}
