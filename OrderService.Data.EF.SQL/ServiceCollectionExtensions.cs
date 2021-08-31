using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
