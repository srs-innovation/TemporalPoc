using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this using directive

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AqsDbContext>(options =>
    options.UseInMemoryDatabase("AqsDb"));    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/createorder", ([FromServices]AqsDbContext context, [FromBody]AqsOrderDto orderDto) =>
{
    try
    {
          if(context.AqsOrders.Any(o => o.CustomerOrderId == orderDto.CustomerOrderId))
    {
        return Results.BadRequest("Order already exists");
    }
    var order = new AqsOrder
    {
        TransactionId = Guid.NewGuid(),
        CustomerId = orderDto.CustomerId,
        CustomerOrderId = orderDto.CustomerOrderId,
        Status = "Processing",
        LineItems = orderDto.LineItems.Select(li => new AqsLineItem
        {
            ProductId = li.ProductId,
            Quantity = li.Quantity,
        }).ToList()
    };
    context.AqsOrders.Add(order);
    context.SaveChanges();
    return Results.Ok();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return Results.BadRequest("Order creation failed");
    } 
})
.WithName("CreateOrder")
.WithOpenApi();

app.MapGet("/getorderstatus", ([FromServices]AqsDbContext context, Guid id) =>
{
    var order = context.AqsOrders.FirstOrDefault(o => o.TransactionId == id);
    if(order == null)
    {
        return Results.NotFound();
    }   
    return Results.Ok(order.Status);
})
.WithName("GetOrderStatus")
.WithOpenApi();

app.MapGet("/getshipping", ([FromServices]AqsDbContext context,Guid id) =>
{
    var order = context.AqsOrders.FirstOrDefault(o => o.TransactionId == id);
    if(order == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(order.Shipping);
})
.WithName("GetShipping")
.WithOpenApi();

app.MapGet("/getinvoice", ([FromServices]AqsDbContext context,Guid id) =>
{
    var order = context.AqsOrders.FirstOrDefault(o => o.TransactionId == id);
    if(order == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(order.Invoice);
})
.WithName("GetInvoice")
.WithOpenApi();

app.MapGet("/getorders", ([FromServices]AqsDbContext context) =>
{
    var orders = context.AqsOrders.Include(c=>c.LineItems).Select(a=> new AqsOrderResponseDto {CustomerId = a.CustomerId, CustomerOrderId = a.CustomerOrderId, LineItems = a.LineItems.Select(b=> new AqsLineResponseItemDto { ProductId = b.ProductId, Quantity = b.Quantity}).ToList(), Status = a.Status, TransactionId = a.TransactionId}).ToListAsync();
    if(orders == null)
    {
        return Results.NotFound();
    }   
    return TypedResults.Ok(orders);
})
.WithName("GetOrders")
.WithOpenApi();

app.Run();