using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulOrderAPI.Data;
using RestfulOrderAPI.Models;

namespace RestfulOrderAPI.Services;

public class OrderService
{
    private readonly OrderContext _context;

    public OrderService(OrderContext context)
    {
        _context = context;
    }

    public IEnumerable<Order> GetAll() => _context
        .Orders.AsNoTracking()
        .ToList();

    public Order? Get(Guid id) => _context
        .Orders.AsNoTracking()
        .SingleOrDefault(o => o.Id == id);

    public Order Add(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
        return order;
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
        if (orderToUpdate is null)
        {
            throw new InvalidOperationException("Order Does Not Exist");
        }

        _context
            .Entry(orderToUpdate)
            .CurrentValues.SetValues(order);

        _context.SaveChanges();

        return orderToUpdate;
    }
}