using Microsoft.AspNetCore.Mvc;
using RestfulOrderAPI.Models;
using RestfulOrderAPI.Services;

namespace RestfulOrderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    // private readonly ILogger<OrderController> _logger;
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost(Name = "CreateOrder")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] Customer customer)
    {
        Order createdOrder = _orderService.Add(customer);
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
    public IActionResult Update(Guid id, OrderCommandObject order)
    {
        if (id != order.Id)
            return BadRequest();

        Order? existingOrder = _orderService.Get(id);
        if (existingOrder is null)
            return NotFound();

        Order updatedOrder = _orderService.Update(order);
        return CreatedAtAction(nameof(Get), new { id = updatedOrder.Id }, updatedOrder);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Delete(Guid id)
    {
        Order? pizza = _orderService.Get(id);

        if (pizza is null)
            return NotFound();

        return _orderService.Delete(id)
            ? Ok()
            : Conflict();
    }

    [HttpGet("Customer/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IEnumerable<Order>? GetOrdersByCustomer(Guid id)
    {
        return _orderService.GetAllByCustomer(id);
    }
}