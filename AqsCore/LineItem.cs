using System.Text.Json.Serialization;

public class LineItem
{
    public string ProductId { get; set; }= string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}


public class AgilityLineItem
{
    [JsonIgnore]
    public int Id { get; set; }
    [JsonIgnore]
    public Guid OrderId { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
}

public class AqsLineItem
{
    [JsonIgnore]
    public int Id { get; set; }
    [JsonIgnore]
    public Guid TransactionId { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
}