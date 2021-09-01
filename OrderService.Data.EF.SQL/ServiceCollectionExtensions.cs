using Microsoft.Extensions.DependencyInjection;

namespace OrderService.Data.EF.SQL
{
    public static class ServiceCollectionExtensions
    {
        public static void AddOrderServiceDataAccess(this IServiceCollection services) =>
            services.AddOrderServiceDbContext();

        public static void AddOrderServiceDbContext(this IServiceCollection services) =>
            services.AddScoped<OrderServiceDbContext>();
    }
}
