using MassTransit;
using Microsoft.EntityFrameworkCore;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Domain.Entities;
using System;
using System.Threading.Tasks;

public class OrderConsumer : IConsumer<ICreateOrderMessage>
{
    private readonly ShopDbContext _dbContext;
    private readonly Random _random = new();

    public OrderConsumer(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<ICreateOrderMessage> context)
    {
        var message = context.Message;

        // Recupera pedido do banco
        var order = await _dbContext.Orders
            .FirstOrDefaultAsync(o => o.Id == message.OrderId);

        if (order == null)
        {
            // Cria novo pedido
            order = new Order
            {
                Id = message.OrderId,
                Status = "Recebido",
                CreatedAt = message.CreatedAt,
                Items = message.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList()
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        // Espera 30s: "Em Processamento"
        await Task.Delay(TimeSpan.FromSeconds(30));
        order.Status = "Em Processamento";
        await _dbContext.SaveChangesAsync();

        // Espera 90s: sorteia status final
        await Task.Delay(TimeSpan.FromSeconds(90));
        bool sucesso = _random.NextDouble() >= 0.5;

        order.Status = sucesso ? "Concluído" : "Falhou";
        await _dbContext.SaveChangesAsync();

        Console.WriteLine($"Pedido {order.Id} → Status: {order.Status}");
    }
}

Console.WriteLine($"Pedido {order.Id} → Status final: {order.Status} | Itens: {order.Items.Count} | Data: {DateTime.Now}");
