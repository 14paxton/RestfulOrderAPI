namespace RestfulOrderAPI.Controllers;

public class OrderCommandObject
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CustomerId { get; set; }
}