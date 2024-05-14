using RestfulOrderAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace RestfulOrderAPI.Controllers;

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
    public IActionResult Create([FromBody] Order newOrder)
    {
        OrderService.Add(newOrder);
        return CreatedAtAction(nameof(Get), newOrder);
    }

    [HttpGet(Name = "GetOrders")]
    public ActionResult<List<Order>> GetAll() => OrderService.GetAll();

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Order> Get(Guid id)
    {
        Order? order = OrderService.Get(id);

        if (order == null)
            return NotFound();

        return order;
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update(Guid id, Order order)
    {
        if (id != order.OrderId)
            return BadRequest();

        Order? existingOrder = OrderService.Get(id);
        if (existingOrder is null)
            return NotFound();

        OrderService.Update(order);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(Guid id)
    {
        Order? pizza = OrderService.Get(id);

        if (pizza is null)
            return NotFound();

        OrderService.Delete(id);

        return NoContent();
    }
}