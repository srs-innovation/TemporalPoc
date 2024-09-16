public class AgilityOrderDto
{
    public string TransactionId { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public List<AgilityLineItemDto> LineItems { get; set; } = new();
}
