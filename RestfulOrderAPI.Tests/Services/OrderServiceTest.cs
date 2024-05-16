using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using RestfulOrderAPI.Controllers;
using RestfulOrderAPI.Data;
using RestfulOrderAPI.Models;
using RestfulOrderAPI.Services;

namespace RestfulOrderAPI.Tests.Services;

[TestFixture]
[TestOf(typeof(OrderService))]
public class OrderServiceTest
{
    [SetUp]
    public void SetUp()
    {
        DbContextOptions<OrderContext> options = new DbContextOptionsBuilder<OrderContext>().UseInMemoryDatabase("TestDB")
            .Options;


        _orderContext = new OrderContext(options);
        _customerService = new CustomerService(_orderContext);
        _orderService = new OrderService(_orderContext, _customerService);
    }

    private OrderContext _orderContext;
    private OrderService _orderService;
    private CustomerService _customerService;

    [Test]
    public async Task GetAllOrders()
    {
        List<string> emailList = ["M14paxton@gmail.com", "mike@mailinator.com", "frank@aol.com"];
        IEnumerable<Customer> customers = emailList.Select(email => new Customer(email));
        IEnumerable<Order> orders = customers.Select(customer => new Order(customer));

        _orderContext.Orders.AddRange(orders);
        await _orderContext.SaveChangesAsync();

        IEnumerable<string> result = _orderService
            .GetAll()
            .Select(o => o.CustomerId)
            .Select(custId => _orderContext.Customers.Find(custId)
                .Email);

        emailList.ForEach(delegate(string email) { Assert.That(true, Is.EqualTo(result.Contains(email))); });
    }

    [Test]
    public async Task CreateAnOrder()
    {
        Customer newOrder = new("test2@google.com");
        Order result = _orderService.Add(newOrder);
        Assert.That(result.CustomerId, Is.EqualTo(newOrder.Id));
    }

    [Test]
    public async Task UpdateAnOrder()
    {
        Customer customer = new("test3@google.com");
        Order result = _orderService.Add(customer);
        DateTime oldCreatedDate = result.CreatedDate;
        Guid id = result.Id;
        OrderCommandObject newOrderData = new()
        {
            Id = id,
            CreatedDate = DateTime.Parse("1922-05-15T18:17:48.782503"),
            CustomerId = customer.Id
        };

        _orderService.Update(newOrderData);
        Order updatedOrder = _orderService.Get(id);

        Assert.That(oldCreatedDate, !Is.EqualTo(updatedOrder.CreatedDate));
    }

    [Test]
    public async Task DeleteAnOrder()
    {
        Customer customer = new("test4@google.com");
        Order result = _orderService.Add(customer);
        Guid id = result.Id;

        _orderContext.SaveChanges();
        _orderContext.ChangeTracker.Clear();

        int beforeOrderCount = _orderService
            .GetAll()
            .Count();

        bool deleted = _orderService.Delete(id);
        Assert.That(deleted, Is.EqualTo(true));

        Order updatedOrder = _orderService.Get(id);
        int afterOrderCount = _orderService
            .GetAll()
            .Count();

        Assert.That(beforeOrderCount - 1, Is.EqualTo(afterOrderCount));
        Assert.That(updatedOrder, Is.EqualTo(null));
    }

    private static DbSet<T> MockDbSet<T>(IEnumerable<T> data) where T : class
    {
        IQueryable<T> queryable = data.AsQueryable();
        Mock<DbSet<T>> mockSet = new();
        mockSet
            .As<IQueryable<T>>()
            .Setup(m => m.Provider)
            .Returns(queryable.Provider);
        mockSet
            .As<IQueryable<T>>()
            .Setup(m => m.Expression)
            .Returns(queryable.Expression);
        mockSet
            .As<IQueryable<T>>()
            .Setup(m => m.ElementType)
            .Returns(queryable.ElementType);
        mockSet
            .As<IQueryable<T>>()
            .Setup(m => m.GetEnumerator())
            .Returns(queryable.GetEnumerator());

        return mockSet.Object;
    }
}