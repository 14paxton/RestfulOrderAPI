using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulOrderAPI.Data;
using RestfulOrderAPI.Models;

namespace RestfulOrderAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly OrderContext _context;

    public CustomerController(OrderContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<Customer>> GetCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Customer>> GetCustomer(Guid id)
    {
        Customer? customer = await _context.Customers.FindAsync(id);

        if (customer == null) return NotFound();

        return customer;
    }
}