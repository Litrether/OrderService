using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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
    public interface IOrderService : IBaseService<Order>
    {
        Task<IReadOnlyCollection<Order>> FindAsync(OrderSearchCondition searchCondition, string sortProperty);
        Task<int> CountAsync(OrderSearchCondition searchCondition);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    }

    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IMongoCollection<Order> _collection;

        public OrderService(IDatabaseContext context)
            : base(context.GetCollection<Order>(nameof(Order)))
        {
            _collection = context.GetCollection<Order>(nameof(Order));
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken) =>
            await _collection.Find(o => o.Id == id).AnyAsync();

        public async Task<IReadOnlyCollection<Order>> FindAsync(OrderSearchCondition searchCondition, string sortProperty)
        {
            IQueryable<Order> query = BuildFindQuery(searchCondition);

            query = searchCondition.SortDirection == "asc"
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.PageSize, searchCondition.Page).ToListAsync();
        }

        public async Task<int> CountAsync(OrderSearchCondition searchCondition)
        {
            IQueryable<Order> query = BuildFindQuery(searchCondition);

            var count = await query.CountAsync();

            return count % 10 == 0 ? count / 10 : count / 10 + 1;
        }

        private IQueryable<Order> BuildFindQuery(OrderSearchCondition searchCondition)
        {
            var query = _collection.AsQueryable();

            if (searchCondition.Status.Any())
                foreach (var status in searchCondition.Status)
                {
                    var upperStatus = status.ToUpper().Trim();
                    query = query.Where(x => x.Status != null && x.Status.ToUpper().Contains(upperStatus));
                }

            if (searchCondition.Cost.Any())
                foreach (var cost in searchCondition.Cost)
                    query = query.Where(x => false);

            if (searchCondition.UserId.Any())
                foreach (var username in searchCondition.UserId)
                {
                    var upperUsername = username.ToUpper().Trim();
                    query = query.Where(x => x.UserId != null && x.UserId.ToUpper().Contains(upperUsername));
                }

            if (searchCondition.DeliveryCompanyId.Any())
                foreach (var deliveryCompany in searchCondition.DeliveryCompanyId)
                    query = query.Where(x => x.DeliveryCompanyId == deliveryCompany);

            if (searchCondition.ProductId.Any())
                foreach (var productId in searchCondition.ProductId)
                    query = query.Where(x => x.ProductId == productId);

            return query;
        }
    }
}
