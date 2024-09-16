namespace AqsCore;

public class AmazonOrder
{
    public int Id { get; set; }
    public string CustomerId { get; set; }  = string.Empty;
    public List<LineItem> LineItems { get; set; } = new();
}
