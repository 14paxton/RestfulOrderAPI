using RestfulOrderAPI.Services;
using Microsoft.AspNetCore.Mvc;
using RestfulOrderAPI.Models;

namespace RestfulOrderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    // private readonly ILogger<OrderController> _logger;
    OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
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
        Order createdOrder = _orderService.Add(newOrder);
        return CreatedAtAction(nameof(Get), new { id = createdOrder.Id }, createdOrder);
    }

    [HttpGet(Name = "GetOrders")]
    public IEnumerable<Order> GetAll()
    {
        return _orderService.GetAll();
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Order> Get(Guid id)
    {
        Order? order = _orderService.Get(id);

        if (order == null)
            return NotFound();

        return order;
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update(Guid id, Order order)
    {
        if (id != order.Id)
            return BadRequest();

        Order? existingOrder = _orderService.Get(id);
        if (existingOrder is null)
            return NotFound();

        _orderService.Update(order);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(Guid id)
    {
        Order? pizza = _orderService.Get(id);

        if (pizza is null)
            return NotFound();

        _orderService.Delete(id);

        return NoContent();
    }
}