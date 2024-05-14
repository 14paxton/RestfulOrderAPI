using Microsoft.AspNetCore.Mvc;

namespace CSharp.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "order1", "order2", "order3", "order4", "order5"
    };

    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("/")]
    public string ShowWorking()
    {
        return "Hello World";
    }

    [HttpGet(Name = "GetOrders")]
    public IEnumerable<Order> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Order
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}