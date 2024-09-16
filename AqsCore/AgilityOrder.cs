using System.ComponentModel.DataAnnotations;
public class AgilityOrder
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string TransactionId { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public List<AgilityLineItem> LineItems { get; set; } = [];
}
