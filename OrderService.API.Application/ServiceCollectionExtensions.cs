using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;
using FluentValidation;
using OrderService.Data.Services.Extensions;
using OrderService.API.Application.Validation.Abstractions;
using System.Linq;

namespace OrderService.API.Application
{
    public static class ServiceCollectionExtensions
    {
        public static void AddOrderServiceApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddOrderServices();
            services.AddValidators();
        }

        private static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.Scan(
                x =>
                {
                    var entryAssembly = Assembly.GetEntryAssembly();
                    IEnumerable<Assembly> referencedAssemblies = entryAssembly.GetReferencedAssemblies().Select(Assembly.Load);
                    IEnumerable<Assembly> assemblies = new List<Assembly> { entryAssembly }.Concat(referencedAssemblies);

                    x.FromAssemblies(assemblies)
                        .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                        .AsImpementedInterfaces()
                        .WithScopedLifetime();
                });

        }
    }
}
