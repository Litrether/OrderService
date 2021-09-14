using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OrderService.API.Contracts.Incoming.SearchConditions;
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
    public interface IDeliveryCompanyService : IBaseService<DeliveryCompany>
    {
        Task<IReadOnlyCollection<DeliveryCompany>> FindAsync(DeliveryCompanySearchCondition searchCondition, string sortProperty);
        Task<int> CountAsync(DeliveryCompanySearchCondition searchCondition);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    }

    public class DeliveryCompanyService : BaseService<DeliveryCompany>, IDeliveryCompanyService
    {
        private readonly IMongoCollection<DeliveryCompany> _collection;

        public DeliveryCompanyService(IDatabaseContext context)
            : base(context.GetCollection<DeliveryCompany>(nameof(DeliveryCompany)))
        {
            _collection = context.GetCollection<DeliveryCompany>(nameof(DeliveryCompany));
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken) =>
            await _collection.Find(o => o.Id == id).AnyAsync();

        public async Task<IReadOnlyCollection<DeliveryCompany>> FindAsync(DeliveryCompanySearchCondition searchCondition, string sortProperty)
        {
            IMongoQueryable<DeliveryCompany> query = BuildFindQuery(searchCondition);

            query = searchCondition.SortDirection == "asc"
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.PageSize, searchCondition.Page).ToListAsync();
        }

        public async Task<int> CountAsync(DeliveryCompanySearchCondition searchCondition = default)
        {
            IMongoQueryable<DeliveryCompany> query = BuildFindQuery(searchCondition);

            return Convert.ToInt32(await query.CountAsync());
        }

        private IMongoQueryable<DeliveryCompany> BuildFindQuery(DeliveryCompanySearchCondition searchCondition = default)
        {
            var query = _collection.AsQueryable();

            if (searchCondition.Name.Any())
                foreach (var name in searchCondition.Name)
                {
                    var upperName = name.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Name != null && x.Name.ToUpper().Contains(upperName)); ;
                }

            if (searchCondition.Rating.Any())
                foreach (var rating in searchCondition.Rating)
                    query = query.Where(x => x.Rating == rating);

            return query;
        }
    }
}
