using CrossCuttingLayer;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OrderService.API.Application;
using OrderService.Data.EF.SQL;
using OrderService.Data.Services.Consumers;
using System;

namespace OrderService.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDatabaseContext, DatabaseContext>();
            services.AddControllers();
            services.AddCors();
            services.AddOrderServiceApplication();
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Order service", Version = "v1" });
            });
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
                    {
                        h.Username(RabbitMqConsts.UserName);
                        h.Password(RabbitMqConsts.Password);
                    });
                    cfg.ReceiveEndpoint("orderQueue", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.ConfigureConsumer<OrderConsumer>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Order service V1"); ;
            });
        }
    }
}
