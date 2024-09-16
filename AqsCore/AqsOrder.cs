using System.ComponentModel.DataAnnotations;

public class AqsOrder
{
    [Key]
    public Guid TransactionId { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public string CustomerOrderId { get; set; } = string.Empty;
    public List<AqsLineItem> LineItems { get; set; } = [];
    public string Status { get; set; } = string.Empty;
    public Guid Shipping { get; set; }
    public Guid Invoice { get; set; }
}


public class AqsOrderDto
{
    public string CustomerId { get; set; } = string.Empty;
    public string CustomerOrderId { get; set; } = string.Empty;
    public List<AqsLineItemDto> LineItems { get; set; } = [];
}


public class AqsOrderResponseDto
{
    public Guid TransactionId { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public string CustomerOrderId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<AqsLineResponseItemDto> LineItems { get; set; } = [];
}


public class AqsLineItemDto
{
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class AqsLineResponseItemDto
{
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
}