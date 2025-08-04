using ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlite("Data Source=ecommerce.db"));

// Swagger e API explorer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Documentação visível apenas em dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products", async (ShopDbContext db) =>
    await db.Products.ToListAsync());

app.Run();

builder.Services.AddScoped<IOrderService, OrderService>();

// Retry config
config.ReceiveEndpoint("order-queue", e =>
{
    e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(60)));
    e.Consumer<OrderConsumer>();
});

