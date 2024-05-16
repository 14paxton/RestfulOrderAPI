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
        _context.Customers.Add(newCustomer);
         await _context.SaveChangesAsync();
         return newCustomer;
    }

    public IEnumerable<Order>? GetCustomerOrders(Guid id)
    {
        Customer customer = _context.Customers.Find(id);
        return customer != null
            ? customer.Orders.ToList()
            : null;
    }
}