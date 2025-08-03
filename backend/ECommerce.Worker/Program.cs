using ECommerce.Worker;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

// Registrando o MassTransit e o Consumer
builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();

    config.AddConsumer<OrderConsumer>()

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");

        cfg.ReceiveEndpoint("order-queue", e =>
        {
            e.ConfigureConsumer<OrderConsumer>(ctx);

            e.UseMessageRetry(r =>
            {
                r.Interval(3, TimeSpan.FromSeconds(60)); // retry de até 3 vezes com 60s de espera
            });
        });
    });
});


builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
