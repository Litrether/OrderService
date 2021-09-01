using Microsoft.Extensions.DependencyInjection;
using OrderService.Data.EF.SQL;
using OrderService.Data.Services.Abstraction;

namespace OrderService.Data.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddOrderServices(this IServiceCollection services)
        {
            services.AddOrderServiceDataAccess();
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            var currentAssembly = typeof(ServiceCollectionExtensions);

            services.Scan(scan => scan.FromAssembliesOf(currentAssembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IBaseService<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );
        }
    }
}
