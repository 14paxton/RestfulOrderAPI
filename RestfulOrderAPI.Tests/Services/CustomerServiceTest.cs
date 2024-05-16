using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RestfulOrderAPI.Data;
using RestfulOrderAPI.Models;
using RestfulOrderAPI.Services;

namespace RestfulOrderAPI.Tests.Services;

[TestFixture]
[TestOf(typeof(CustomerService))]
public class CustomerServiceTest
{
    private OrderContext _orderContext;
    private CustomerService _customerService;
    private OrderService _orderService;

    [SetUp]
    public void SetUp()
    {
        DbContextOptions<OrderContext> options = new DbContextOptionsBuilder<OrderContext>().UseInMemoryDatabase("TestDB")
            .Options;

        _orderContext = new OrderContext(options);
        _customerService = new CustomerService(_orderContext);
        _orderService = new OrderService(_orderContext, _customerService);
    }

    [Test]
    public void CreateCustomer()
    {
        List<string> emailList = ["M14paxton@gmail.com", "mike@mailinator.com", "frank@aol.com"];
        IEnumerable<Customer> customers = emailList.Select(email => new Customer(email));

        foreach (Customer customer in customers)
        {
            _customerService.CreateCustomer(customer);
        }

        Assert.That(3, Is.EqualTo(_orderContext.Customers.Count()));
    }

    [Test]
    public async Task GetAllCustomerOrders()
    {
        List<string> emailList = ["Test2@gmail.com", "Test2@mailinator.com", "Test2@aol.com"];
        IEnumerable<Customer> customers = emailList.Select(email => new Customer(email));
        IEnumerable<Order> orders = customers.Select(customer => new Order(customer));
        _orderContext.SaveChanges();

        ICollection<Guid> customerIdList = new List<Guid>();

        foreach (Guid guid in customerIdList)
        {
            int orderListCount = _orderContext.Customers.Find(guid)
                .Orders.Count;

            Assert.That(3, Is.EqualTo(orderListCount));
        }
    }
}