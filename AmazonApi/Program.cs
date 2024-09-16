using AqsCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


var orders = new List<AmazonOrder>
{
    new () { Id = 1, CustomerId = "1", LineItems = new List<LineItem> { new () { ProductId = "1", Quantity = 1 } } },
    new () { Id = 2, CustomerId = "2", LineItems = new List<LineItem> { new () { ProductId = "2", Quantity = 2 } } },
    new () { Id = 3, CustomerId = "3", LineItems = new List<LineItem> { new () { ProductId = "3", Quantity = 3 } } },
    new () { Id = 4, CustomerId = "4", LineItems = new List<LineItem> { new () { ProductId = "4", Quantity = 4 } } },
    new () { Id = 5, CustomerId = "5", LineItems = new List<LineItem> { new () { ProductId = "5", Quantity = 5 } } }
};

app.MapGet("/getorders", () =>
{
    return orders;
})
.WithName("GetOrders")
.WithOpenApi();

app.Run();

