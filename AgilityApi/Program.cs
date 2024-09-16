using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AgilityDbContext>(options =>
    options.UseInMemoryDatabase("AgilityDb"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/createorder", async ([FromServices] AgilityDbContext context, [FromBody]AgilityOrderDto orderDto) =>
{
    try
    {
        if(context.Orders.Any(o => o.TransactionId == orderDto.TransactionId))
        {
            return Results.BadRequest("Order already exists");
        }
        var order = new AgilityOrder
        {
            Id = Guid.NewGuid(),
            TransactionId = orderDto.TransactionId,
            CustomerId = orderDto.CustomerId,
            LineItems = orderDto.LineItems.Select(li => new AgilityLineItem
            {
                ProductId = li.ProductId,
                Quantity = li.Quantity
            }).ToList()
        };
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return Results.Created($"/getorder/{order.Id}", order);   
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.BadRequest("Order creation failed");
    }
})
.WithName("CreateOrder")
.WithOpenApi();

app.MapGet("/getshipping", (Guid id) =>
{
    return Results.Ok(Guid.NewGuid().ToString());
})
.WithName("GetShipping")
.WithOpenApi();

app.MapGet("/getinvoice", (Guid id) =>
{
    return Results.Ok(Guid.NewGuid().ToString());
})
.WithName("GetInvoice")
.WithOpenApi();

app.MapGet("/getorder", ([FromServices] AgilityDbContext context,Guid id) =>
{
    try
    {
        var order = context.Orders.Include(o => o.LineItems).FirstOrDefault(o => o.Id == id);
        if(order == null)
        {
            return Results.NotFound();
        }
        var result =  new AgilityOrderDto
        {
            TransactionId = order.TransactionId,
            CustomerId = order.CustomerId,
            LineItems = order.LineItems.Select(li => new AgilityLineItemDto
            {
                ProductId = li.ProductId,
                Quantity = li.Quantity
            }).ToList()
        };
        
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.BadRequest("Order retrieval failed");
    }
})
.WithName("GetOrder")
.WithOpenApi();

app.Run();



