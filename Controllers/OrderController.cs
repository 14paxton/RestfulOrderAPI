using Microsoft.AspNetCore.Mvc;

namespace CSharp.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private static readonly string[] Orders = new[]
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
        return "Hello World!";
    }

    [HttpPost(Name = "CreateOrder")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Order> Create([FromBody] Order newOrder)
    {
        return CreatedAtAction(newOrder.Id.ToString(), newOrder);
    }

    [HttpGet(Name = "GetOrders")]
    public IEnumerable<Order> Get()
    {
        return Enumerable
            .Range(1, 5)
            .Select(index => new Order
            {
                Id = Guid.NewGuid(),
                Email = Orders[Random.Shared.Next(Orders.Length)]
            })
            .ToArray();
    }
}