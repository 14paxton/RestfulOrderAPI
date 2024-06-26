using Microsoft.EntityFrameworkCore;
using RestfulOrderAPI.Data;
using RestfulOrderAPI.Models;

namespace RestfulOrderAPI.Services;

public class CustomerService
{
    private readonly OrderContext _context;

    public CustomerService(OrderContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public bool CustomerExists(string email)
    {
        return _context.Customers.Any(e => e.Email == email);
    }

    public async Task<Customer> CreateCustomer(Customer newCustomer)
    {
        if (CustomerExists(newCustomer.Email))
        {
            return _context.Customers.Single(c => c.Email == newCustomer.Email);
        }

        _context.Customers.Add(newCustomer);
        await _context.SaveChangesAsync();
        return newCustomer;
    }

    public IEnumerable<Order>? GetCustomerOrders(Guid id)
    {
        Customer customer = _context
            .Customers
            .Include(p => p.Orders)
            .First(p => p.Id == id);

        return customer.Orders;
    }
}