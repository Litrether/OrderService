﻿using CrossCuttingLayer;
using CrossCuttingLayer.Models;
using MassTransit;
using OrderService.Data.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data.Services.Consumers
{
    public class OrderConsumer : IConsumer<OrderMessage>
    {
        public IOrderService _orderService { get; set; }
        public IDeliveryCompanyService _deliveryCompanyService { get; set; }

        public OrderConsumer(IOrderService orderService,
            IDeliveryCompanyService deliveryCompanyService)
        {
            _orderService = orderService;
            _deliveryCompanyService = deliveryCompanyService;
        }

        public async Task Consume(ConsumeContext<OrderMessage> context)
        {
            var message = context.Message;
            var function = message.Function;
            switch (function)
            {
                case CrossFunction.POST:
                    var order = MapToOrder(context.Message);
                    await CreateOrderAsync(order);
                    break;
                case CrossFunction.DELETE:
                    await DeleteOrderAsync(message.Id.Value);
                    break;
            }
        }

        private async Task CreateOrderAsync(Order order) =>
            await _orderService.CreateAsync(order);

        private async Task DeleteOrderAsync(int orderId)
        {
            if (await _orderService.ExistsAsync(orderId))
                await _orderService.DeleteAsync(orderId);
        }

        private Order MapToOrder(OrderMessage order) =>  
            new Order
            {
                Status = "Processed",
                Cost = order.Cost.Value,
                Username = order.Username,
                ProductId = order.ProductId.Value,
                DeliveryCompanyId = 1,
            };
    }
}
