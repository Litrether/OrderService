using Microsoft.EntityFrameworkCore;
using OrderService.Data.Domain.Models;
using OrderService.Data.EF.SQL;
using OrderService.Data.Services.Abstraction;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Data.Services
{
    public interface IOrderService : IBaseService<Order>
    {
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    }

    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly OrderServiceDbContext dbContext;

        public OrderService(OrderServiceDbContext dbContext) : base(dbContext) =>
            this.dbContext = dbContext;

        public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken) =>
            dbContext.Orders.AnyAsync(entity => entity.Id == id, cancellationToken);
    }
}
