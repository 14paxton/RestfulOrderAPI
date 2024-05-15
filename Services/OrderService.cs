#region

using Microsoft.EntityFrameworkCore;
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
            .Orders.AsNoTracking()
            .SingleOrDefault(o => o.Id == id);
    }

    public Order Add(Customer customer)
    {
        if (!_customerService.CustomerExists(customer.Email))
        {
            _ = _customerService.CreateCustomer(customer);
        }

        Order newOrder = new(customer);
        _context.Orders.Add(newOrder);
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
            _context.Orders.Remove(orderToDelete);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            returnValue = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InvalidOperationException("Unable to Delete");
        }

        return returnValue;
    }

    public Order Update(Order order)
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