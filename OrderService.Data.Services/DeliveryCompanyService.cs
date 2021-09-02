using Microsoft.EntityFrameworkCore;
using OrderService.API.Contracts;
using OrderService.Data.Domain.Models;
using OrderService.Data.EF.SQL;
using OrderService.Data.Services.Abstraction;
using OrderService.Data.Services.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Data.Services
{
    public interface IDeliveryCompanyService : IBaseService<DeliveryCompany>
    {
        Task<IReadOnlyCollection<DeliveryCompany>> FindAsync(OrderSearchCondition searchCondition, string sortProperty);
        Task<int> CountAsync(OrderSearchCondition searchCondition);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    }

    public class DeliveryCompanyService : BaseService<Order>, IOrderService
    {
        private readonly OrderServiceDbContext dbContext;

        public DeliveryCompanyService(OrderServiceDbContext dbContext) : base(dbContext) =>
            this.dbContext = dbContext;

        public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken) =>
            dbContext.DeliveryCompanies.AnyAsync(entity => entity.Id == id, cancellationToken);

        public async Task<IReadOnlyCollection<Order>> FindAsync(OrderSearchCondition searchCondition, string sortProperty)
        {
            IQueryable<Order> query = BuildFindQuery(searchCondition);

            query = searchCondition.ListSortDirection == ListSortDirection.Ascending
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.Page, searchCondition.PageSize).ToListAsync();
        }

        public async Task<int> CountAsync(OrderSearchCondition searchCondition)
        {
            IQueryable<Order> query = BuildFindQuery(searchCondition);

            return await query.CountAsync();
        }

        private IQueryable<Order> BuildFindQuery(OrderSearchCondition searchCondition)
        {
            IQueryable<Order> query = dbContext.Orders;

            //todo searchConditions delivery

            return query;
        }
    }
}
