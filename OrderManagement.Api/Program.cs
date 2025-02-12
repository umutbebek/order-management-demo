
using Microsoft.EntityFrameworkCore;
using OrderManagement.Business.Order;
using OrderManagement.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<OrderManagementContext>(options =>
{
    options.UseInMemoryDatabase("OrderManagementContextDb");
}, ServiceLifetime.Singleton);
builder.Services.AddScoped<IOrderBusiness, OrderBusiness>();
builder.Services.AddScoped<IOrderMessageBusiness, OrderMessageBusiness>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(builderC =>
{
    builderC
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Seeder.Seed(app.Services.GetService<OrderManagementContext>());

app.Run();

