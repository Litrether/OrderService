using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
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
        private readonly IDatabaseContext _context;
        private readonly IMongoCollection<DeliveryCompany> _collection;

        public OrderService(IDatabaseContext context) : base(context)
        {
            _context = context;
            _collection = _context.GetCollection<DeliveryCompany>(nameof(DeliveryCompany));
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken) =>
            await _collection.Find(o => o.Id == id).AnyAsync();
    }
}
