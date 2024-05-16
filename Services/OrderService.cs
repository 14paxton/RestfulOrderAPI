#region

using Microsoft.EntityFrameworkCore;
using RestfulOrderAPI.Controllers;
using RestfulOrderAPI.Data;
using RestfulOrderAPI.Models;

#endregion

namespace RestfulOrderAPI.Services;

public class OrderService
{
    private readonly OrderContext _context;
    private readonly CustomerService _customerService;

    public OrderService(OrderContext context, CustomerService customerService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _customerService = customerService;
    }

    public IEnumerable<Order> GetAll()
    {
        return _context
            .Orders.AsNoTracking()
            .ToList();
    }

    public Order? Get(Guid id)
    {
        return _context
            .Orders
            .SingleOrDefault(o => o.Id == id);
    }

    public Order Add(Customer customer)
    {
        Customer savedCustomer = _customerService.CustomerExists(customer.Email)
            ? _context
                .Customers
                .Single(c => c.Email == customer.Email)
            : _customerService.CreateCustomer(customer)
                .Result;

        Order newOrder = new(savedCustomer);
        _context.Orders.Add(newOrder);
        savedCustomer.Orders.Add(newOrder);
        _context.SaveChanges();
        return newOrder;
    }

    public bool Delete(Guid id)
    {
        bool returnValue = false;
        Order? orderToDelete = _context.Orders.Find(id);
        if (orderToDelete is null)
            return returnValue;

        try
        {
            Customer? parent = _context
                .Customers
                .Include(p => p.Orders)
                .FirstOrDefault(p => p.Id == orderToDelete.CustomerId);

            Order? child = parent.Orders.FirstOrDefault(c => c.Id == id);
            parent.Orders.Remove(child);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            returnValue = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InvalidOperationException($"Error Deleting: {e}");
        }

        return returnValue;
    }

    public Order Update(OrderCommandObject order)
    {
        Order? orderToUpdate = _context.Orders.Find(order.Id);
        if (orderToUpdate is null) throw new InvalidOperationException("Order Does Not Exist");

        _context
            .Entry(orderToUpdate)
            .CurrentValues.SetValues(order);

        _context.SaveChanges();

        return orderToUpdate;
    }

    public IEnumerable<Order>? GetAllByCustomer(Guid customerId)
    {
        return _customerService.GetCustomerOrders(customerId);
    }
}